variable "companyName" {
  type        = string
  description = "(opcional) Nome da empresa. Padrão: \"boaentrega\""
  default     = "boaentrega"
}

variable "micAppName" {
  type        = string
  description = "(opcional) Nome da API do Módulo de Informações Cadastrais"
  default     = "moduloinformacoescadastraisapi"
}

variable "publisherEmail" {
  type        = string
  description = "(opcional) O e-mail do publisher da API Management admin."
  default     = "gabrielatba@hotmail.com"
}

variable "publisherName" {
  type        = string
  description = "(opcional) O nome do publisher."
  default     = "Empresa \"Boa Entrega\""
}

variable "subscriptionId" {
  type        = string
  description = "(opcional) O subscription id onde todos os recursos serão criados. Padrão: \"f63ea9a1-6b21-4536-a566-b7e50aee50cb\""
  default     = "f63ea9a1-6b21-4536-a566-b7e50aee50cb"
}

variable "tenantId" {
  type        = string
  description = "(opcional) O tenant id da aplicação. Padrão: \"b56bdd3f-e9bc-4072-bab4-2191ce42dc0e\""
  default     = "b56bdd3f-e9bc-4072-bab4-2191ce42dc0e"
}

variable "userReaderEmail" {
  type        = string
  description = "(opcional) O e-mail de um usuário com perfil reader da API do Módulo de Informações Cadastrais."
  default     = "gabstba@gmail.com"
}
