using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ModuloInformacoesCadastrais.API.OpenApi
{
    public class AppendAuthorizeToSummaryAppOperationFilter : IOperationFilter
    {
        private readonly AppendAuthorizeToSummaryAppOperationFilter<AuthorizeAttribute> filter;

        public AppendAuthorizeToSummaryAppOperationFilter()
        {
            var policySelector = new PolicySelectorWithLabel<AuthorizeAttribute>
            {
                Label = "policies",
                Selector = authAttributes =>
                    authAttributes
                        .Where(a => !string.IsNullOrEmpty(a.Policy))
                        .Select(a => a.Policy)
            };

            var rolesSelector = new PolicySelectorWithLabel<AuthorizeAttribute>
            {
                Label = "roles",
                Selector = authAttributes =>
                    authAttributes
                        .Where(a => !string.IsNullOrEmpty(a.Roles))
                        .Select(a => a.Roles)
            };

            filter = new AppendAuthorizeToSummaryAppOperationFilter<AuthorizeAttribute>(new[] { policySelector, rolesSelector }.AsEnumerable());
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            filter.Apply(operation, context);
        }
    }

    public class AppendAuthorizeToSummaryAppOperationFilter<T> : IOperationFilter where T : Attribute
    {
        private readonly IEnumerable<PolicySelectorWithLabel<T>> policySelectors;

        /// <summary>
        /// Constructor for AppendAuthorizeToSummaryOperationFilter
        /// </summary>
        /// <param name="policySelectors">Used to select the authorization policy from the attribute e.g. (a => a.Policy)</param>
        public AppendAuthorizeToSummaryAppOperationFilter(IEnumerable<PolicySelectorWithLabel<T>> policySelectors)
        {
            this.policySelectors = policySelectors;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (GetControllerAndActionAttributes<AllowAnonymousAttribute>(context).Any())
            {
                return;
            }

            var authorizeAttributes = GetControllerAndActionAttributes<T>(context);

            if (authorizeAttributes.Any())
            {
                var authorizationDescription = new StringBuilder(" (Auth");

                foreach (var policySelector in policySelectors)
                {
                    AppendPolicies(authorizeAttributes, authorizationDescription, policySelector);
                }

                operation.Summary += authorizationDescription.ToString().TrimEnd(';') + ")";
            }
        }

        private void AppendPolicies(IEnumerable<T> authorizeAttributes, StringBuilder authorizationDescription, PolicySelectorWithLabel<T> policySelector)
        {
            var policies = policySelector.Selector(authorizeAttributes)
                .OrderBy(policy => policy);

            if (policies.Any())
            {
                authorizationDescription.Append($" {policySelector.Label}: {string.Join(", ", policies)};");
            }
        }

        public static IEnumerable<T> GetControllerAndActionAttributes<T>(OperationFilterContext context) where T : Attribute
        {
            if (context.MethodInfo != null)
            {
                var controllerAttributes = context.MethodInfo.DeclaringType?.GetTypeInfo().GetCustomAttributes<T>();
                var actionAttributes = context.MethodInfo.GetCustomAttributes<T>();

                var result = new List<T>(actionAttributes);
                if (controllerAttributes != null)
                {
                    result.AddRange(controllerAttributes);
                }

                return result;
            }

            if (context.ApiDescription.ActionDescriptor.EndpointMetadata != null)
            {
                var endpointAttributes = context.ApiDescription.ActionDescriptor.EndpointMetadata.OfType<T>();

                var result = new List<T>(endpointAttributes);
                return result;
            }
            return Enumerable.Empty<T>();
        }
    }

    public class PolicySelectorWithLabel<T> where T : Attribute
    {
        public Func<IEnumerable<T>, IEnumerable<string>> Selector { get; set; }

        public string Label { get; set; }
    }
}