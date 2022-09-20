using CleanArchitectureBlazor.WebUI.Shared.Authorization;

namespace CleanArchitectureBlazor.WebUI.Shared.AccessControl;

public class RoleDto
{
    public RoleDto()
    {
        Id = string.Empty;
        Name = string.Empty;
        Permissions = Permissions.None;
    }

    public RoleDto(string id, string name, Permissions permissions)
    {
        Id = id;
        Name = name;
        Permissions = permissions;
    }

    public string Id { get; set; }

    public string Name { get; set; }

    public Permissions Permissions { get; set; }

    public bool Has(Permissions permission)
    {
        return Permissions.HasFlag(permission); ;
    }

    public void Set(Permissions permission, bool granted)
    {
        if (granted)
        {
            Grant(permission);
        }
        else
        {
            Revoke(permission);
        }
    }

    public void Grant(Permissions permission)
    {
        Permissions |= permission;
    }

    public void Revoke(Permissions permission)
    {
        Permissions ^= permission;
    }
}