resource "azurerm_application_insights" "boaentrega" {
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
  name                  = "api-mic"
  resource_group_name   = var.resourceGroupName
  api_management_name   = azurerm_api_management.boaentrega.name
  revision              = "1"
  display_name          = "API Módulo de Informações Cadastrais"
  path                  = "mic"
  protocols             = ["https"]
  subscription_required = false

  import {
    content_format = "openapi-link"
    content_value  = "https://${var.micServiceName}.azurewebsites.net/swagger/v2/docs.json"
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
        <validate-jwt header-name="Authorization" failed-validation-httpcode="401" failed-validation-error-message="Unauthorized. Access token is missing or invalid.">
            <openid-config url="https://login.microsoftonline.com/${var.tenantId}/v2.0/.well-known/openid-configuration" />
            <required-claims>
                <claim name="aud">
                    <value>${var.micAppApplicationClientId}</value>
                </claim>
            </required-claims>
        </validate-jwt>
  </inbound>
</policies>
XML
}

resource "azurerm_api_management_logger" "boaentrega" {
  name                = "${var.companyName}-logger"
  api_management_name = azurerm_api_management.boaentrega.name
  resource_group_name = var.resourceGroupName

  application_insights {
    instrumentation_key = azurerm_application_insights.boaentrega.instrumentation_key
  }
}

resource "azurerm_api_management_api_diagnostic" "mic" {
  identifier               = "applicationinsights"
  resource_group_name      = var.resourceGroupName
  api_management_name      = azurerm_api_management.boaentrega.name
  api_name                 = azurerm_api_management_api.mic.name
  api_management_logger_id = azurerm_api_management_logger.boaentrega.id

  sampling_percentage       = 100.0
  always_log_errors         = true
  log_client_ip             = true
  verbosity                 = "verbose"
  http_correlation_protocol = "W3C"

  frontend_request {
    body_bytes = 8192
    headers_to_log = [
      "content-type",
      "accept",
      "origin",
      "authorization"
    ]
  }

  frontend_response {
    body_bytes = 8192
    headers_to_log = [
      "content-type",
      "content-length",
      "origin"
    ]
  }

  backend_request {
    body_bytes = 8192
    headers_to_log = [
      "content-type",
      "accept",
      "origin",
      "authorization"
    ]
  }

  backend_response {
    body_bytes = 8192
    headers_to_log = [
      "content-type",
      "content-length",
      "origin"
    ]
  }
}
