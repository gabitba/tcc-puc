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
    content_value  = "http://${var.micServiceName}.azurewebsites.net/${var.micApiPath}"
  }
}

resource "azurerm_api_management_backend" "mic" {
  name                = "backend-mic"
  resource_group_name = var.resourceGroupName
  api_management_name = azurerm_api_management.boaentrega.name
  protocol            = "http"
  url                 = "http://${var.micServiceName}.azurewebsites.net/"
}
