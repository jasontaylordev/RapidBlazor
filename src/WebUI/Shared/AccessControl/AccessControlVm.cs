using CleanArchitectureBlazor.WebUI.Shared.Authorization;

namespace CleanArchitectureBlazor.WebUI.Shared.AccessControl;

public class AccessControlVm
{
    public AccessControlVm() { }

    public AccessControlVm(IList<RoleDto> roles)
    {
        Roles = roles;

        foreach (var permission in PermissionsProvider.GetAll())
        {
            if (permission == Permissions.None) continue;

            AvailablePermissions.Add(permission);
        }
    }

    public IList<RoleDto> Roles { get; set; } = new List<RoleDto>();

    public IList<Permissions> AvailablePermissions { get; set; } = new List<Permissions>();
}
