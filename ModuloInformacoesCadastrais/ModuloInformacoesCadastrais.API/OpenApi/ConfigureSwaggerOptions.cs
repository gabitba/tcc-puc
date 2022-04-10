using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ModuloInformacoesCadastrais.API.OpenApi
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
                Title = "API Modulo de Informações Cadastrais",
                Version = description.ApiVersion.ToString(),
                Description = "Uma aplicação .NET 6 Web API para acesso aos dados cadastrais.",
                Contact = new OpenApiContact { Name = "Gabriela Tolentino", Email = "gabrielatba@gmail.com" }
            };

            if (description.IsDeprecated)
                info.Description += " Esta versão da API está descontinuada.";

            return info;
        }
    }
}