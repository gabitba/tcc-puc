resource "azurerm_application_insights" "apiboaentrega" {
  name                = "apiboaentregaappinsights"
  location            = var.location
  resource_group_name = var.resourceGroupName
  workspace_id        = var.logAnalyticsWorkspaceId
  application_type    = "other"
  tags = {
    "terraform" = "true"
  }
}

resource "azurerm_api_management" "boaentrega" {
  name                = "api${var.companyName}"
  location            = var.location
  resource_group_name = var.resourceGroupName
  publisher_name      = var.companyDisplayName
  publisher_email     = var.publisherEmail

  sku_name = "Consumption_0"

  identity {
    type = "SystemAssigned"
  }

  tags = {
    "terraform" = "true"
  }
}

resource "azurerm_api_management_api" "mic" {
  name                = "api-mic"
  resource_group_name = var.resourceGroupName
  api_management_name = azurerm_api_management.boaentrega.name
  revision            = "1"
  display_name        = "API Módulo de Informações Cadastrais"
  path                = "mic"
  protocols           = ["https"]

  import {
    content_format = "openapi-link"
    content_value  = "https://${var.micServiceName}.azurewebsites.net/swagger/v1/swagger.json"
  }
}

resource "azurerm_api_management_backend" "mic" {
  name                = "WebApp_${var.micServiceName}"
  resource_group_name = var.resourceGroupName
  api_management_name = azurerm_api_management.boaentrega.name
  description         = var.micServiceName
  title               = "MIC Backend"
  resource_id         = "https://management.azure.com/subscriptions/${var.subscriptionId}/resourceGroups/${var.resourceGroupName}/providers/Microsoft.Web/sites/${var.micServiceName}"
  protocol            = "http"
  url                 = "http://${var.micServiceName}.azurewebsites.net/"
}

resource "azurerm_api_management_policy" "mic" {
  api_management_id = azurerm_api_management.boaentrega.id
  xml_content       = <<XML
<policies>
  <inbound/>
  <backend>
    <forward-request/>
  </backend>
  <outbound/>
</policies>
XML
}

resource "azurerm_api_management_api_policy" "mic" {
  api_name            = azurerm_api_management_api.mic.name
  api_management_name = azurerm_api_management_api.mic.api_management_name
  resource_group_name = azurerm_api_management_api.mic.resource_group_name

  xml_content = <<XML
<policies>
  <inbound>
    <base />
    <set-backend-service id="set-backend-service-policy" backend-id="${azurerm_api_management_backend.mic.name}" />
  </inbound>
</policies>
XML
}
