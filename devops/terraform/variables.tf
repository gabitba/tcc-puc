variable "companyName" {
  type        = string
  description = "(optional) The name of the company. Default: \"boaentrega\""
  default     = "boaentrega"
}

variable "micAppName" {
  type        = string
  description = "(optional) The name of the MIC app service"
  default     = "moduloinformacoescadastraisapi"
}

variable "companyDisplayName" {
  type        = string
  description = "(optional) The display name of the company. Default: \"Boa Entrega\""
  default     = "Boa Entrega"
}

variable "publisherEmail" {
  type        = string
  description = "(optional) The e-mail of the API Management publisher admin."
  default     = "gabrielatba@hotmail.com"
}

variable "subscriptionId" {
  type        = string
  description = "(optional) The subscription id where all the resources will be created. Default: \"f63ea9a1-6b21-4536-a566-b7e50aee50cb\""
  default     = "f63ea9a1-6b21-4536-a566-b7e50aee50cb"
}

variable "tenantId" {
  type        = string
  description = "(optional) The tenant of the application. Default: \"b56bdd3f-e9bc-4072-bab4-2191ce42dc0e\""
  default     = "b56bdd3f-e9bc-4072-bab4-2191ce42dc0e"
}
