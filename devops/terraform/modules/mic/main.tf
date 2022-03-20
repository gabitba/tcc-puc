resource "azurerm_application_insights" "mic" {
  name                = "apm-mic"
  location            = var.location
  resource_group_name = var.resourceGroupName
  workspace_id        = var.logAnalyticsWorkspaceId
  application_type    = "web"
  tags = {
    "terraform" = "true"
  }
}

resource "azurerm_app_service_plan" "mic" {
  name                = "asp-mic"
  location            = var.location
  resource_group_name = var.resourceGroupName
  kind                = "Linux"
  reserved            = true

  sku {
    tier = "Basic"
    size = "B1"
  }

  tags = {
    "terraform" = "true"
  }
}

resource "azurerm_app_service" "mic" {
  name                = "app-mic"
  location            = var.location
  resource_group_name = var.resourceGroupName
  app_service_plan_id = azurerm_app_service_plan.mic.id

  site_config {
    dotnet_framework_version = "v6.0"
    linux_fx_version         = "DOTNETCORE|6.0"
    cors {
      allowed_origins = ["*"]
    }
  }

  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY"             = azurerm_application_insights.mic.instrumentation_key
    "APPLICATIONINSIGHTS_CONNECTION_STRING"      = "InstrumentationKey=${azurerm_application_insights.mic.instrumentation_key};IngestionEndpoint=https://${var.location}-3.in.applicationinsights.azure.com/"
    "ApplicationInsightsAgent_EXTENSION_VERSION" = "~3"
    "XDT_MicrosoftApplicationInsights_Mode"      = "Recommended"
  }

  tags = {
    "terraform" = "true"
  }
}
