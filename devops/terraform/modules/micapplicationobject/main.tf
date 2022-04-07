data "azuread_client_config" "current" {}

data "azuread_application_published_app_ids" "well_known" {}

resource "azuread_service_principal" "msgraph" {
  application_id = data.azuread_application_published_app_ids.well_known.result.MicrosoftGraph
  use_existing   = true
}

resource "azuread_application" "mic_app" {
  display_name     = "${var.companyDisplayName} MIC App"
  identifier_uris  = ["api://${var.companyName}-micapi"]
  owners           = [data.azuread_client_config.current.object_id]
  sign_in_audience = "AzureADMyOrg"

  api {
    mapped_claims_enabled          = true
    requested_access_token_version = 2

    oauth2_permission_scope {
      admin_consent_description  = "Allow the application to access the ${var.companyDisplayName} MIC App on behalf of the user."
      admin_consent_display_name = "Access ${var.companyDisplayName} MIC App"
      enabled                    = true
      id                         = "c38ee7b0-6e7e-4741-82a4-ed4aeb1bbcfe"
      type                       = "User"
      user_consent_description   = "Allow the application to access the ${var.companyDisplayName} MIC App on your behalf."
      user_consent_display_name  = "Access ${var.companyDisplayName} MIC App"
      value                      = "user_impersonation"
    }

    oauth2_permission_scope {
      admin_consent_description  = "Allow the application to read client information."
      admin_consent_display_name = "Read client information"
      enabled                    = true
      id                         = "3762efdb-f992-4cb3-bc2c-e0b9bed2d3e0"
      type                       = "User"
      user_consent_description   = "Allow the application to read client information."
      user_consent_display_name  = "Read client information"
      value                      = "Client.Read"
    }

    oauth2_permission_scope {
      admin_consent_description  = "Allow the application to write client information."
      admin_consent_display_name = "Write client information"
      enabled                    = true
      id                         = "792c1c1d-7c8f-48c0-9a8d-a3438abc99a2"
      type                       = "Admin"
      value                      = "Client.Write"
    }
  }

  required_resource_access {
    resource_app_id = data.azuread_application_published_app_ids.well_known.result.MicrosoftGraph

    resource_access {
      id   = azuread_service_principal.msgraph.oauth2_permission_scope_ids["openid"]
      type = "Scope"
    }

    resource_access {
      id   = azuread_service_principal.msgraph.oauth2_permission_scope_ids["User.Read"]
      type = "Scope"
    }
  }

  app_role {
    allowed_member_types = ["Application"]
    description          = "Allow the application to read client information."
    display_name         = "Read client information"
    enabled              = true
    id                   = "ad8af6cf-161e-4eae-bfce-b78845d6c904"
    value                = "Client.Read.All"
  }

  app_role {
    allowed_member_types = ["Application"]
    description          = "Allow the application to write client information."
    display_name         = "Read write information"
    enabled              = true
    id                   = "b22a8058-8146-4aa9-a016-058fc6e5542c"
    value                = "Client.Write.All"
  }

  web {
    logout_url    = "https://${var.micServiceName}.azurewebsites.net/.auth/logout"
    redirect_uris = ["https://${var.micServiceName}.azurewebsites.net/.auth/login/aad/callback", "https://localhost:7013/swagger/oauth2-redirect.html"]
    implicit_grant {
      access_token_issuance_enabled = true
      id_token_issuance_enabled     = true
    }
  }
}

resource "azuread_service_principal" "mic_app" {
  application_id = azuread_application.mic_app.application_id
  login_url      = "https://${var.micServiceName}.azurewebsites.net/.auth/login/aad"
  owners         = [data.azuread_client_config.current.object_id]
}

resource "azuread_service_principal_delegated_permission_grant" "mic_app" {
  service_principal_object_id          = azuread_service_principal.mic_app.object_id
  resource_service_principal_object_id = azuread_service_principal.msgraph.object_id
  claim_values                         = ["openid", "User.Read.All"]
}
