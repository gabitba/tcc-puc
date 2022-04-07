output "micAppApplicationClientId" {
  value = azuread_application.mic_app.application_id
}

output "micAppServicePrincipalClientId" {
  value = azuread_service_principal.mic_app.application_id
}
