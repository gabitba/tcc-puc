using Microsoft.Extensions.Options;
using ModuloServicosCliente.Application.DTOs;
using ModuloServicosCliente.Application.Interfaces;
using ModuloServicosCliente.Infra.Options;
using System.Net.Http.Json;

namespace ModuloServicosCliente.Infra.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient httpClient;

        public ClienteService(HttpClient httpClient, IOptions<ClienteAPIOptions> options)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        }

        public async Task<ClienteDTO> ObterClienteAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<ClienteDTO>($"api/v1/Clientes/{id}");
        }
    }
}
