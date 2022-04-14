using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;

namespace ModuloInformacoesCadastrais.API.Authorization
{
    public static class ConfigureAuthorization
    {
        public static IServiceCollection AddAndConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(configuration, jwtBearerScheme: JwtBearerDefaults.AuthenticationScheme);
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationResources.Clientes.WRITE_POLICY_NAME, p =>
                {
                    p.AddRequirements(new RoleRequirement( new string[] { AuthorizationResources.Clientes.Role.Write }));
                });

                options.AddPolicy(AuthorizationResources.Clientes.READ_OR_WRITE_POLICY_NAME, p =>
                {
                    p.AddRequirements(new RoleRequirement(new string[] { AuthorizationResources.Clientes.Role.Read, AuthorizationResources.Clientes.Role.Write }));
                });
            });

            services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();

            services.AddOptions<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme).Configure(options =>
            {
                options.TokenValidationParameters.RoleClaimType = "roles";
            });

            return services;
        }
    }
}