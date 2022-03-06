using System.ComponentModel.DataAnnotations;

namespace ModuloInformacoesCadastrais.Domain.Entidades
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }
    }
}
