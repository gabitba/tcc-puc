using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModuloEmail.API
{
    public class EmailService
    {
        private readonly IOptions<EmailOptions> options;
        private readonly HttpClient httpClient;

        public EmailService(HttpClient httpClient, IOptions<EmailOptions> options)
        {
            this.options = options;
            this.httpClient = httpClient;
            httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        }

        public async Task EnviarEmailAsync(EmailConteudo email)
        {
            if(string.IsNullOrWhiteSpace(email.To))
            {
                email.To = options.Value.DestinatarioDefault;
            }

            HttpContent content = new StringContent(JsonSerializer.Serialize(email), Encoding.ASCII, "application/json");
            await httpClient.PostAsync("", content);
        }

        public class EmailConteudo
        {
            [JsonPropertyName("to")]
            public string To { get; set; } = string.Empty;

            [JsonPropertyName("subject")]
            public string Subject { get; set; } = string.Empty;

            [JsonPropertyName("isHtml")]
            public bool IsHtml { get; set; }

            [JsonPropertyName("body")]
            public string Body { get; set; } = string.Empty;
        }
    }
}