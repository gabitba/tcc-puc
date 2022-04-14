using Microsoft.AspNetCore.Authorization;

namespace ModuloInformacoesCadastrais.API.Authorization
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> Roles { get; set; }

        public RoleRequirement(IEnumerable<string> roles)
        {
            Roles = roles ?? Array.Empty<string>();
        }
    }
}