﻿namespace ModuloInformacoesCadastrais.API.Models
{
    public class ClienteModel
    {
        public int Id { get; internal set; }

        public string Nome { get; internal set; }

        public object Endereco { get; internal set; }
    }
}