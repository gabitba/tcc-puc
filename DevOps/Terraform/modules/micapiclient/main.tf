data "azuread_client_config" "current" {}

data "azuread_application_published_app_ids" "well_known" {}

resource "azuread_service_principal" "msgraph" {
  application_id = data.azuread_application_published_app_ids.well_known.result.MicrosoftGraph
  use_existing   = true
}

resource "azuread_service_principal" "mic_app" {
  application_id = var.micAppClientId
  use_existing   = true
}

resource "azuread_application" "mic_app_read_client" {
  display_name     = "MIC App Client Reader"
  owners           = [data.azuread_client_config.current.object_id]
  sign_in_audience = "AzureADMyOrg"

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

  required_resource_access {
    resource_app_id = azuread_service_principal.mic_app.application_id

    resource_access {
      id   = azuread_service_principal.mic_app.app_role_ids["Clientes.Reader"]
      type = "Role"
    }
  }
}

resource "azuread_service_principal" "mic_app_read_client" {
  application_id = azuread_application.mic_app_read_client.application_id
  owners         = [data.azuread_client_config.current.object_id]
}

resource "azuread_service_principal_delegated_permission_grant" "mic_app_read_client_msgraph" {
  service_principal_object_id          = azuread_service_principal.mic_app_read_client.object_id
  resource_service_principal_object_id = azuread_service_principal.msgraph.object_id
  claim_values                         = ["openid", "User.Read.All"]
}

resource "azuread_service_principal_delegated_permission_grant" "mic_app_read_client_micapp" {
  service_principal_object_id          = azuread_service_principal.mic_app_read_client.object_id
  resource_service_principal_object_id = azuread_service_principal.mic_app.object_id
  claim_values                         = ["Clientes.Reader"]
}

resource "azuread_application" "mic_app_write_client" {
  display_name     = "MIC App Client Writer"
  owners           = [data.azuread_client_config.current.object_id]
  sign_in_audience = "AzureADMyOrg"

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

  required_resource_access {
    resource_app_id = azuread_service_principal.mic_app.application_id

    resource_access {
      id   = azuread_service_principal.mic_app.app_role_ids["Clientes.Writer"]
      type = "Role"
    }
  }
}

resource "azuread_service_principal" "mic_app_write_client" {
  application_id = azuread_application.mic_app_write_client.application_id
  owners         = [data.azuread_client_config.current.object_id]
}

resource "azuread_service_principal_delegated_permission_grant" "mic_app_write_client_msgraph" {
  service_principal_object_id          = azuread_service_principal.mic_app_write_client.object_id
  resource_service_principal_object_id = azuread_service_principal.msgraph.object_id
  claim_values                         = ["openid", "User.Read.All"]
}

resource "azuread_service_principal_delegated_permission_grant" "mic_app_write_client_micapp" {
  service_principal_object_id          = azuread_service_principal.mic_app_write_client.object_id
  resource_service_principal_object_id = azuread_service_principal.mic_app.object_id
  claim_values                         = ["Clientes.Writer"]
}