using Microsoft.AspNetCore.Authorization;

namespace RapidBlazor.WebUi.Shared.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        PermissionAuthorizationRequirement requirement)
    {
        var permissionClaim = context.User.FindFirst(
            c => c.Type == CustomClaimTypes.Permissions);

        if (permissionClaim is null)
        {
            return Task.CompletedTask;
        }

        if (!int.TryParse(permissionClaim.Value, out var permissionClaimValue))
        {
            return Task.CompletedTask;
        }

        var userPermissions = (Permissions)permissionClaimValue;

        if ((userPermissions & requirement.Permission) != 0)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}
