variable "companyName" {
  type        = string
  description = "(required) The name of the company."
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
