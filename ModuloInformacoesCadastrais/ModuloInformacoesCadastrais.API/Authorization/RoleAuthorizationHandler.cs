using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ModuloInformacoesCadastrais.API.Authorization
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        private IEnumerable<string> ROLE_CLAIM_NAMES = new string[] { ClaimTypes.Role, "roles" };

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (!requirement.Roles.Any())
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (UserHasClaimValue(requirement.Roles, GetClaims(context.User, ROLE_CLAIM_NAMES)))
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