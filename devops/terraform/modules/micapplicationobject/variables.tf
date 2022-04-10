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

variable "micServiceName" {
  type        = string
  description = "(required) The name of the Mic App Service."
}
