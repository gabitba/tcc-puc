variable "companyName" {
  type        = string
  description = "(optional) The name of the company. Default: \"boaentrega\""
  default     = "boaentrega"
}

variable "companyDisplayName" {
  type        = string
  description = "(optional) The display name of the company. Default: \"Boa Entrega\""
  default     = "Boa Entrega"
}

variable "subscriptionId" {
  type        = string
  description = "(required) The Azure Subscription Id where the resources are created."
}

variable "resourceGroupName" {
  type        = string
  description = "(required) The name of the resource group which will contain the resources."
}

variable "location" {
  type        = string
  description = "(required) Location where the resources will be created."
}

variable "logAnalyticsWorkspaceId" {
  type        = string
  description = "(required) Id of the Log Analytics Workspace resource which will be used by this resource."
}

variable "publisherEmail" {
  type        = string
  description = "(required) The e-mail of the API Management publisher admin."
}

variable "micServiceName" {
  type        = string
  description = "(required) The name of the Mic App Service."
}

variable "micApiPath" {
  type        = string
  description = "(required) The path of the api exposed by the Mic App Service."
}
