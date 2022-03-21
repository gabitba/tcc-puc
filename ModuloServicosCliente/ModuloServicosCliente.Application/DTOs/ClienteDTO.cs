using System.Text.Json.Serialization;

namespace ModuloServicosCliente.Application.DTOs
{
    public class ClienteDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("endereco")]
        public string Endereco { get; set; }
    }
}
