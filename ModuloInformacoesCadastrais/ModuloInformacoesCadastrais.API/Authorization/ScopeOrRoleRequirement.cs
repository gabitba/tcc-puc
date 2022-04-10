using Microsoft.AspNetCore.Authorization;

namespace ModuloInformacoesCadastrais.API.Authorization
{
    public class ScopeOrRoleRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> Scopes { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public ScopeOrRoleRequirement(IEnumerable<string> scopes, IEnumerable<string> roles)
        {
            this.Scopes = scopes ?? Array.Empty<string>();
            this.Roles = roles ?? Array.Empty<string>();
        }
    }
}