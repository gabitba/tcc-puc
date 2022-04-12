using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ModuloEmail.API.OpenApi
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (ApiVersionDescription description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            OpenApiInfo info = new OpenApiInfo()
            {
                Title = "API Modulo de E-mail",
                Version = description.ApiVersion.ToString(),
                Description = "Uma aplicação .NET 6 Web API para envio de e-mails por meio de um Azure Logic Apps. O Azure Logic Apps está configurado para que ao receber uma requisição HTTP, ele envie um e-mail com as informações passadas no corpo da requisição.",
                Contact = new OpenApiContact { Name = "Gabriela Tolentino", Email = "gabrielatba@gmail.com" }
            };

            if (description.IsDeprecated)
                info.Description += " Esta versão da API está descontinuada.";

            return info;
        }
    }
}
