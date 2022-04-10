using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;

namespace ModuloInformacoesCadastrais.API.Authorization
{
    public static class ConfigureAuthorization
    {
        public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                .AddMicrosoftIdentityWebApi(configuration, jwtBearerScheme: JwtBearerDefaults.AuthenticationScheme);
            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationResources.Client.WRITE_POLICY_NAME, p =>
                {
                    p.AddRequirements(new ScopeOrRoleRequirement(new string[] { AuthorizationResources.Client.Scope.Write }, new string[] { AuthorizationResources.Client.Role.Write }));
                });
                options.AddPolicy(AuthorizationResources.Client.READ_OR_WRITE_POLICY_NAME, p =>
                {
                    p.AddRequirements(new ScopeOrRoleRequirement(new string[] { AuthorizationResources.Client.Scope.Read, AuthorizationResources.Client.Scope.Write }, new string[] { AuthorizationResources.Client.Role.Read, AuthorizationResources.Client.Role.Write }));
                });
            });

            services.AddSingleton<IAuthorizationHandler, ScopeOrRoleAuthorizationHandler>();

            services.AddOptions<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme).Configure(options =>
            {
                options.TokenValidationParameters.RoleClaimType = "roles";
            });

            return services;
        }
    }
}