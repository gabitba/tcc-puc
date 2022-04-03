using Microsoft.Extensions.Options;
using ModuloServicosCliente.Application.DTOs;
using ModuloServicosCliente.Application.Interfaces;
using ModuloServicosCliente.Infra.Options;
using System.Text;
using System.Text.Json;

namespace ModuloServicosCliente.Infra.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient httpClient;

        public EmailService(HttpClient httpClient, IOptions<EmailAPIOptions> options)
        {
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        }

        public async Task EnviarEmailReportCliente(string destinatario, ClienteDTO cliente)
        {
            var request = new EnviarEmailReportClienteModelRequest
            {
                Destinatario = destinatario,
                Cliente = cliente,
            };

            HttpContent content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/Email/ReportCliente", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Falha ao enviar e-mail: {response.Content}.");
            }
        }

        internal class EnviarEmailReportClienteModelRequest
        {
            public string? Destinatario { get; set; }

            public ClienteDTO Cliente { get; set; }
        }
    }
}
