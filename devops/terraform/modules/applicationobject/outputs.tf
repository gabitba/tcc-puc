output "appClientId" {
  value = azuread_application.client_app.application_id
}

output "servicePrincipalClientId" {
  value = azuread_service_principal.client_app.application_id
}
