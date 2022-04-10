using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ModuloInformacoesCadastrais.API.Authorization
{
    public class ScopeOrRoleAuthorizationHandler : AuthorizationHandler<ScopeOrRoleRequirement>
    {
        private IEnumerable<string> ROLE_CLAIM_NAMES = new string[] { ClaimTypes.Role, "roles" };
        private IEnumerable<string> SCOPE_CLAIM_NAMES = new string[] { "scp", "http://schemas.microsoft.com/identity/claims/scope" };

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeOrRoleRequirement requirement)
        {
            if (!requirement.Roles.Any() && !requirement.Scopes.Any())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (UserHasClaimValue(requirement.Roles, GetClaims(context.User, ROLE_CLAIM_NAMES))
                || UserHasClaimValue(requirement.Scopes, GetClaims(context.User, SCOPE_CLAIM_NAMES)))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

        private IEnumerable<string> GetClaims(ClaimsPrincipal user, IEnumerable<string> possibleClaimNames)
        {
            if (user.HasClaim(c => possibleClaimNames.Contains(c.Type)))
            {
                return user.Claims.First(c => possibleClaimNames.Contains(c.Type))!.Value.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            }

            return Array.Empty<string>();
        }

        private bool UserHasClaimValue(IEnumerable<string> requirementValues, IEnumerable<string> userValues)
        {
            return requirementValues.Intersect(userValues).Any();
        }
    }
}