using CleanArchitecture.WebUI.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CleanArchitecture.WebUI.Shared.Authorization;

public static class IAuthorizationServiceExtensions
{
    public static Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService service, ClaimsPrincipal user, Permissions permissions)
    {
        return service.AuthorizeAsync(user, PolicyNameHelper.GeneratePolicyNameFor(permissions));
    }
}
