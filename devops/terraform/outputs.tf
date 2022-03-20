output "mic_instrumentation_key" {
  value     = azurerm_application_insights.mic.instrumentation_key
  sensitive = true
}
