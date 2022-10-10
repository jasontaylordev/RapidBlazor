using Microsoft.AspNetCore.Authorization;

namespace RapidBlazor.WebUI.Shared.Authorization;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public PermissionAuthorizationRequirement(Permissions permission)
    {
        Permissions = permission;
    }

    public Permissions Permissions { get; }
}
