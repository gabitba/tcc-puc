using System.ComponentModel.DataAnnotations;

namespace ModuloServicosCliente.API.Models
{
    public class StartInstanciaProcessoRequestModel
    {
        [Required]
        public string IdProcesso { get; set; } = string.Empty;

        public Dictionary<string, string> Variaveis { get; set; } = new Dictionary<string, string>();
    }

    public class StartInstanciaProcessoResponseModel
    {
        public string IdProcesso { get; internal set; } = string.Empty;
        public long KeyInstancia { get; internal set; }
        public int Versao { get; internal set; }
        public long KeyDefinicaoProcesso { get; internal set; }
    }
}
