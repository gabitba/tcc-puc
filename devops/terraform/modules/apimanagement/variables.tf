variable "companyName" {
  type        = string
  description = "(opcional) Nome da empresa. Padrão: \"boaentrega\""
  default     = "boaentrega"
}

variable "subscriptionId" {
  type        = string
  description = "(obrigatório) O Azure Subscription Id onde os recursos são criados."
}

variable "tenantId" {
  type        = string
  description = "(obrigatório) O Azure AD Tenant Id da empresa."
}

variable "resourceGroupName" {
  type        = string
  description = "(obrigatório) O nome do grupo de recurso que conterá os recursos."
}

variable "location" {
  type        = string
  description = "(obrigatório) Location onde os recursos serão criados."
}

variable "logAnalyticsWorkspaceId" {
  type        = string
  description = "(obrigatório) Id do recurso de Log Analytics Workspace o qual vai ser usado por este recurso."
}

variable "publisherEmail" {
  type        = string
  description = "(obrigatório) O e-mail do publisher da API Management admin."
}

variable "micAppName" {
  type        = string
  description = "(obrigatório) The name of the Mic App Service."
}

variable "micAppApplicationClientId" {
  type        = string
  description = "(obrigatório) Nome da API do Módulo de Informações Cadastrais"
}

variable "micApiPath" {
  type        = string
  description = "(obrigatório) O caminho o qual a API é exposta no serviço do Módulo de Informações Cadastrais."
}
