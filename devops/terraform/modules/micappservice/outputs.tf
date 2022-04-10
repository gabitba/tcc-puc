output "mic_instrumentation_key" {
  value     = azurerm_application_insights.mic.instrumentation_key
  sensitive = true
}

output "appName" {
  value = azurerm_app_service.mic.name
}

output "appId" {
  value = azurerm_app_service.mic.id
}
