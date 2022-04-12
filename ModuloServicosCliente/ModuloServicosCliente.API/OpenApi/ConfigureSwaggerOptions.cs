using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ModuloServicosCliente.API.OpenApi
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
                Title = "API Modulo de Serviços ao Cliente",
                Version = description.ApiVersion.ToString(),
                Description = "Uma aplicação .NET 6 Web API para automatização de processos de serviço ao Cliente baseados em BPMN no Camunda.",
                Contact = new OpenApiContact { Name = "Gabriela Tolentino", Email = "gabrielatba@gmail.com" }
            };

            if (description.IsDeprecated)
                info.Description += " Esta versão da API está descontinuada.";

            return info;
        }
    }
}
