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

variable "publisherEmail" {
  type        = string
  description = "(optional) The e-mail of the API Management publisher admin."
  default     = "gabrielatba@hotmail.com"
}
