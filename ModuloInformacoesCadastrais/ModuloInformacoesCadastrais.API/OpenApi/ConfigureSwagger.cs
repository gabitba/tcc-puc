using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ModuloInformacoesCadastrais.API.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ModuloInformacoesCadastrais.API.OpenApi
{
    public static class ConfigureSwagger
    {
        public static void AddOpenApiSupport(this IServiceCollection services, IConfiguration configuration)
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
                string authBaseUrl = $"{configuration["AzureAD:Instance"]}/{configuration["AzureAD:TenantId"]}/oauth2/v2.0";
                string audience = "api://boaentrega-micapi";
                options.OperationFilter<OperationDefaultValues>();
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
                            TokenUrl = new Uri($"{authBaseUrl}/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { $"{audience}/{AuthorizationResources.Client.Scope.Read}", "Read clients" },
                                { $"{audience}/{AuthorizationResources.Client.Scope.Write}", "Write clients" },
                            }
                        }
                    }
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] {
                            AuthorizationResources.Client.Scope.Read,
                            AuthorizationResources.Client.Scope.Write,
                            AuthorizationResources.Client.Role.Read,
                            AuthorizationResources.Client.Role.Write
                        }
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
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