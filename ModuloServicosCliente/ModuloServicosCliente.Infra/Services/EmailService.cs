using Microsoft.Extensions.Options;
using ModuloServicosCliente.Application.DTOs;
using ModuloServicosCliente.Application.Interfaces;
using ModuloServicosCliente.Infra.Options;
using System.Text;
using System.Text.Json;

namespace ModuloServicosCliente.Infra.Services
{
    internal class EmailService : IEmailService
    {
        private readonly HttpClient httpClient;

        public EmailService(HttpClient httpClient, IOptions<EmailAPIOptions> options)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        }

        public async Task EnviarEmailReportCliente(ClienteDTO cliente)
        {
            var request = new EnviarEmailReportClienteModelRequest
            {
                Cliente = cliente,
            };

            HttpContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            await httpClient.PostAsync("Email/ReportCliente", content);
        }

        internal class EnviarEmailReportClienteModelRequest
        {
            public string? Destinatario { get; set; }

            public ClienteDTO Cliente { get; set; }
        }
    }
}
