using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace ModuloEmail.API.OpenApi
{
    public static class ConfigureSwagger
    {
        public static void AddAndConfigureOpenApiSupport(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                options.OperationFilter<OperationDefaultValues>();
            });
        }

        public static void UseSwaggerApp(this WebApplication app)
        {
            app.UseSwagger(options => { options.RouteTemplate = "swagger/{documentName}/docs.json"; });
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "swagger";
                foreach (ApiVersionDescription description in app.Services.GetRequiredService<IApiVersionDescriptionProvider>().ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/docs.json", description.GroupName.ToUpperInvariant());
                }
                options.OAuthClientId(app.Configuration["AzureAD:ClientId"]);
                options.OAuthUsePkce();
                options.OAuthScopeSeparator(" ");
            });
        }
    }
}
