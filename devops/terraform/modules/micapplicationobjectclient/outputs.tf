output "micAppReadClientApplicationClientId" {
  value = azuread_application.mic_app_read_client.application_id
}

output "micAppWriteClientServicePrincipalClientId" {
  value = azuread_service_principal.mic_app_write_client.application_id
}
