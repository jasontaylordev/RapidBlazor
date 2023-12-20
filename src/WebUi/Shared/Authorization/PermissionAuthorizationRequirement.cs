using Microsoft.AspNetCore.Authorization;

namespace RapidBlazor.WebUi.Shared.Authorization;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public PermissionAuthorizationRequirement(Permissions permission)
    {
        Permission = permission;
    }
    
    public Permissions Permission { get; }
}
