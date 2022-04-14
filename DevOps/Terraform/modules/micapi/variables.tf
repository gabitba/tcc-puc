variable "companyName" {
  type        = string
  description = "(opcional) Nome da empresa. Padrão: \"boaentrega\"."
  default     = "boaentrega"
}

variable "micAppName" {
  type        = string
  description = "(opcional) Nome da API do Módulo de Informações Cadastrais."
  default     = "moduloinformacoescadastraisapi"
}
