using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RapidBlazor.WebUi.Shared.Authorization;
using System.Security.Claims;

namespace RapidBlazor.Infrastructure.Identity;

public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
{
    public ApplicationUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<IdentityOptions> options)
        : base(userManager, roleManager, options)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        var userRolesName = await UserManager.GetRolesAsync(user);

        var userRoles = await RoleManager.Roles.Where(r =>
            userRolesName.Contains(r.Name ?? string.Empty)).ToListAsync();

        var userPermissions = Permissions.None;

        foreach (var role in userRoles)
            userPermissions |= role.Permissions;

        var permissionValue = (int)userPermissions;
        
        identity.AddClaim(new Claim(CustomClaimTypes.Permissions, permissionValue.ToString()));

        return identity;
    }
}
