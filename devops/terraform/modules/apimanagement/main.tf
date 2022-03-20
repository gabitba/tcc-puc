resource "azurerm_application_insights" "apiboaentrega" {
  name                = "apm-apiboaentrega"
  location            = var.location
  resource_group_name = var.resourceGroupName
  workspace_id        = var.logAnalyticsWorkspaceId
  application_type    = "other"
  tags = {
    "terraform" = "true"
  }
}

resource "azurerm_api_management" "boaentrega" {
  name                = "api-${var.companyName}"
  location            = var.location
  resource_group_name = var.resourceGroupName
  publisher_name      = var.companyDisplayName
  publisher_email     = var.publisherEmail

  sku_name = "Consumption_0"

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
  protocols           = ["https", "http"]

  import {
    content_format = "swagger-link-json"
    content_value  = "http://conferenceapi.azurewebsites.net/?format=json"
    # content_value  = "http://${var.micServiceName}.azurewebsites.net/${var.micApiPath}?format=json"
  }
}

resource "azurerm_api_management_backend" "mic" {
  name                = "backend-mic"
  resource_group_name = var.resourceGroupName
  api_management_name = azurerm_api_management.boaentrega.name
  title               = "MIC Backend"
  resource_id         = "https://management.azure.com/subscriptions/${var.subscriptionId}/resourceGroups/${var.resourceGroupName}/providers/Microsoft.Web/sites/${var.micServiceName}"
  protocol            = "http"
  url                 = "http://${var.micServiceName}.azurewebsites.net/"
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
