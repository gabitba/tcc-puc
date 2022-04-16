using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ModuloInformacoesCadastrais.API.OpenApi
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
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                string authBaseUrl = $"{configuration["AzureAD:Instance"]}/{configuration["AzureAD:TenantId"]}/oauth2/v2.0";

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "OAuth2.0 Auth Code with PKCE",
                    Name = "oauth2",
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{authBaseUrl}/authorize"),
                            Scopes = new Dictionary<string, string>
                            {
                                { $"{configuration["AzureAD:ClientId"]}/.default", "Default" },
                            }
                        },
                        ClientCredentials = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{authBaseUrl}/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { $"{configuration["AzureAD:ClientId"]}/.default", "Default" },
                            }
                        }
                    }
                });
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