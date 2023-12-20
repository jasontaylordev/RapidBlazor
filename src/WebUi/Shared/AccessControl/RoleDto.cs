using RapidBlazor.WebUi.Shared.Authorization;

namespace RapidBlazor.WebUi.Shared.AccessControl;

public sealed class RoleDto
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

    public string Id { get; }
    
    public string Name { get; set; }
    
    public Permissions Permissions { get; set; }

    public bool Has(Permissions permission)
    {
        return Permissions.HasFlag(permission);
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

    private void Grant(Permissions permission)
    {
        Permissions |= permission;
    }

    public void Revoke(Permissions permission)
    {
        Permissions ^= permission;
    }
}
