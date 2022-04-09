using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

using ModuloInformacoesCadastrais.API.Authorization;

using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using ModuloInformacoesCadastrais.Infra.IoC;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

InjetorDeDependencias.ConfigurarDependencias(builder.Services, builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddMicrosoftIdentityWebApi(builder.Configuration, jwtBearerScheme: JwtBearerDefaults.AuthenticationScheme);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AuthorizationResources.Client.READ_POLICY_NAME, p =>
    {
        p.AddRequirements(new ScopeOrRoleRequirement(new string[] { AuthorizationResources.Client.Scope.Read }, new string[] { AuthorizationResources.Client.Role.Read }));
    });
    options.AddPolicy(AuthorizationResources.Client.READ_OR_WRITE_POLICY_NAME, p =>
    {
        p.AddRequirements(new ScopeOrRoleRequirement(new string[] { AuthorizationResources.Client.Scope.Read, AuthorizationResources.Client.Scope.Write }, new string[] { AuthorizationResources.Client.Role.Read, AuthorizationResources.Client.Role.Write }));
    });
});
builder.Services.AddSingleton<IAuthorizationHandler, ScopeOrRoleAuthorizationHandler>();

builder.Services.AddOptions<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme)
                .Configure(options =>
                    {
                        //options.TokenValidationParameters.NameClaimType = "name";
                        options.TokenValidationParameters.RoleClaimType = "roles";
                    });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    string authBaseUrl = $"{builder.Configuration["AzureAD:Instance"]}/{builder.Configuration["AzureAD:TenantId"]}/oauth2/v2.0";
    string audience = "api://boaentrega-micapi";
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Modulo de Informações Cadastrais",
        Description = "Uma aplicação .NET 6 Web API para acesso aos dados cadastrais.",
        Contact = new OpenApiContact
        {
            Name = "Gabriela Tolentino",
        }
    });
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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.OAuthClientId(builder.Configuration["AzureAD:ClientId"]);
    options.OAuthUsePkce();
    options.OAuthScopeSeparator(" ");
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
