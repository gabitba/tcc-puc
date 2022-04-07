using System.ComponentModel.DataAnnotations;

namespace ModuloInformacoesCadastrais.API.Models
{
    public class CadastrarClienteRequestModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Endereco { get; set; }
    }

    public class CadastrarClienteResponseModel
    {
        public int id { get; set; }
    }
}
