data "azuread_client_config" "current" {}

data "azuread_application_published_app_ids" "well_known" {}

resource "azuread_service_principal" "msgraph" {
  application_id = data.azuread_application_published_app_ids.well_known.result.MicrosoftGraph
  use_existing   = true
}

resource "azuread_application" "mic_app" {
  display_name     = "Módulo Informações Cadastrais App"
  identifier_uris  = ["api://${var.companyName}-micapi"]
  owners           = [data.azuread_client_config.current.object_id]
  sign_in_audience = "AzureADMyOrg"

  api {
    mapped_claims_enabled          = true
    requested_access_token_version = 2
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
    allowed_member_types = ["User", "Application"]
    description          = "Permite leitura de dados de Clientes."
    display_name         = "Ler dados de Clientes"
    enabled              = true
    id                   = "ad8af6cf-161e-4eae-bfce-b78845d6c904"
    value                = "Clientes.Reader"
  }

  app_role {
    allowed_member_types = ["User", "Application"]
    description          = "Permite leitura e escrita de dados de Clientes."
    display_name         = "Ler e escrever dados de Clientes."
    enabled              = true
    id                   = "b22a8058-8146-4aa9-a016-058fc6e5542c"
    value                = "Clientes.Writer"
  }

  web {
    logout_url = "https://${var.micAppName}.azurewebsites.net/.auth/logout"
    redirect_uris = [
      "https://${var.micAppName}.azurewebsites.net/.auth/login/aad/callback",
      "https://${var.micAppName}.azurewebsites.net/swagger/oauth2-redirect.html",
      "https://localhost:7013/swagger/oauth2-redirect.html",
      "https://oauth.pstmn.io/v1/browser-callback"
    ]
    implicit_grant {
      access_token_issuance_enabled = true
      id_token_issuance_enabled     = true
    }
  }
}

resource "azuread_service_principal" "mic_app" {
  application_id = azuread_application.mic_app.application_id
  login_url      = "https://${var.micAppName}.azurewebsites.net/.auth/login/aad"
  owners         = [data.azuread_client_config.current.object_id]
}

resource "azuread_service_principal_delegated_permission_grant" "mic_app" {
  service_principal_object_id          = azuread_service_principal.mic_app.object_id
  resource_service_principal_object_id = azuread_service_principal.msgraph.object_id
  claim_values                         = ["openid", "User.Read.All"]
}


data "azuread_domains" "ad_domains" {
  only_initial = true
}


resource "azuread_user" "reader" {
  display_name        = "Gabs Reader"
  password            = "Senh@!2022"
  user_principal_name = "gabs.reader@${data.azuread_domains.ad_domains.domains.0.domain_name}"
}

resource "azuread_app_role_assignment" "reader" {
  app_role_id         = azuread_service_principal.mic_app.app_role_ids["Clientes.Reader"]
  principal_object_id = azuread_user.reader.object_id
  resource_object_id  = azuread_service_principal.mic_app.object_id
}

resource "azuread_user" "writer" {
  display_name        = "Gabs Writer"
  password            = "Senh@!2022"
  user_principal_name = "gabs.writer@${data.azuread_domains.ad_domains.domains.0.domain_name}"
}

resource "azuread_app_role_assignment" "writer" {
  app_role_id         = azuread_service_principal.mic_app.app_role_ids["Clientes.Writer"]
  principal_object_id = azuread_user.writer.object_id
  resource_object_id  = azuread_service_principal.mic_app.object_id
}
