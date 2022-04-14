variable "companyName" {
  type        = string
  description = "(opcional) Nome da empresa. Padrão: \"boaentrega\"."
  default     = "boaentrega"
}

variable "micAppName" {
  type        = string
  description = "(opcional) Nome da API do Módulo de Informações Cadastrais."
}

variable "micAppClientId" {
  type        = string
  description = "(obrigatório) O client id do registro da aplicação do Módulo de Informações Cadastrais."
}