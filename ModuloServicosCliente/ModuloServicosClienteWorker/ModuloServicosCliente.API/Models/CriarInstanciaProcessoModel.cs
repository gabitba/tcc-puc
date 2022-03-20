using System.ComponentModel.DataAnnotations;

namespace ModuloServicosCliente.API.Models
{
    public class CriarInstanciaProcessoRequestModel
    {
        [Required]
        public string IdProcesso { get; set; } = string.Empty;

        public Dictionary<object, object> Variaveis { get; set; } = new Dictionary<object, object>();
    }

    public class ComecarInstanciaProcessoResponseModel
    {
        public string IdProcesso { get; internal set; } = string.Empty;
        public long KeyInstancia { get; internal set; }
        public int Versao { get; internal set; }
        public long KeyDefinicaoProcesso { get; internal set; }
    }
}
