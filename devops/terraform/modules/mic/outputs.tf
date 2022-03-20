output "mic_instrumentation_key" {
  value     = azurerm_application_insights.mic.instrumentation_key
  sensitive = true
}

output "apiName" {
  value = azurerm_app_service.mic.name
}
