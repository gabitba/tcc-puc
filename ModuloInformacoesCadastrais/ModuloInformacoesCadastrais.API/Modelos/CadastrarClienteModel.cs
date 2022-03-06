using System.ComponentModel.DataAnnotations;

namespace ModuloInformacoesCadastrais.API.Models
{
    public class CadastrarClienteModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Endereco { get; set; }
    }
}
