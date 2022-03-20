terraform {
  backend "azurerm" {
    resource_group_name  = "TCC-PUC"
    storage_account_name = "tccpuc"
    container_name       = "terraformstate"
    key                  = "prod.terraform.tfstate"
    client_id            = "b5acc16c-d659-47cc-9b41-0f1730ea14d5"
    client_secret        = "B-G7Q~0fXurm9CDTNYUelbSnAOxeOh5eI~fvy"
    subscription_id      = "f63ea9a1-6b21-4536-a566-b7e50aee50cb"
    tenant_id            = "b56bdd3f-e9bc-4072-bab4-2191ce42dc0e"
    use_microsoft_graph  = true
  }

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=2.99.0"
    }
  }

  required_version = ">= 1.1.0"
}

provider "azurerm" {
  skip_provider_registration = true
  use_msal                   = true
  features {
    api_management {
      purge_soft_delete_on_destroy = true
    }
    log_analytics_workspace {
      permanently_delete_on_destroy = true
    }
  }
}

data "azurerm_resource_group" "tccpuc" {
  name = "TCC-PUC"
}

resource "azurerm_log_analytics_workspace" "boaentrega" {
  name                = "law-${var.companyName}"
  location            = data.azurerm_resource_group.tccpuc.location
  resource_group_name = data.azurerm_resource_group.tccpuc.name
  retention_in_days   = 30
  tags = {
    "terraform" = "true"
  }
}

resource "azurerm_application_insights" "apiboaentrega" {
  name                = "apm-apiboaentrega"
  location            = data.azurerm_resource_group.tccpuc.location
  resource_group_name = data.azurerm_resource_group.tccpuc.name
  workspace_id        = azurerm_log_analytics_workspace.boaentrega.id
  application_type    = "other"
  tags = {
    "terraform" = "true"
  }
}

resource "azurerm_application_insights" "mic" {
  name                = "apm-mic"
  location            = data.azurerm_resource_group.tccpuc.location
  resource_group_name = data.azurerm_resource_group.tccpuc.name
  workspace_id        = azurerm_log_analytics_workspace.boaentrega.id
  application_type    = "web"
  tags = {
    "terraform" = "true"
  }
}
