using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebUI.Shared.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
    {
        var permissionClaim = context.User.FindFirst(
            c => c.Type == CustomClaimTypes.Permissions);

        if (permissionClaim == null)
        {
            return Task.CompletedTask;
        }

        if (!int.TryParse(permissionClaim.Value, out int permissionClaimValue))
        {
            return Task.CompletedTask;
        }

        var userPermissions = (Permissions)permissionClaimValue;

        if ((userPermissions & requirement.Permissions) != 0)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}
