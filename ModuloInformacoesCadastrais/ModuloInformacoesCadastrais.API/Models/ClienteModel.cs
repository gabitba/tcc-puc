using ModuloInformacoesCadastrais.Application.DTOs;
using System.Text.Json.Serialization;

namespace ModuloInformacoesCadastrais.API.Models
{
    public class ClienteModel
    {
        [JsonPropertyName("id")]
        public int Id { get; internal set; }

        [JsonPropertyName("nome")]
        public string Nome { get; internal set; }

        [JsonPropertyName("endereco")]
        public string Endereco { get; internal set; }

        public static ClienteModel ConvertToModel(ClienteDTO clienteDTO)
        {
            return new ClienteModel
            {
                Id = clienteDTO.Id,
                Nome = clienteDTO.Nome,
                Endereco = clienteDTO.Endereco
            };
        }
    }
}
