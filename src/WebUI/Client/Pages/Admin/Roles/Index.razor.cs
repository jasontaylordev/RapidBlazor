using Microsoft.AspNetCore.Components;
using RapidBlazor.WebUI.Shared.AccessControl;
using RapidBlazor.WebUI.Shared.Authorization;

namespace RapidBlazor.WebUI.Client.Pages.Admin.Roles;

public partial class Index
{
    [Inject]
    public IRolesClient RolesClient { get; set; } = null!;

    public RolesVm? Model { get; set; }

    private string newRoleName = string.Empty;

    private RoleDto? roleToEdit;

    protected override async Task OnInitializedAsync()
    {
        Model = await RolesClient.GetRolesAsync();
    }

    private async Task AddRole()
    {
        if (!string.IsNullOrWhiteSpace(newRoleName))
        {
            var newRole = new RoleDto(Guid.NewGuid().ToString(), newRoleName, Permissions.None);

            await RolesClient.PostRoleAsync(newRole);

            Model!.Roles.Add(newRole);
        }

        newRoleName = string.Empty;
    }

    private void EditRole(RoleDto role)
    {
        roleToEdit = role;
    }

    private void CancelEditRole()
    {
        roleToEdit = null;
    }

    private async Task UpdateRole()
    {
        await RolesClient.PutRoleAsync(roleToEdit!.Id, roleToEdit);

        roleToEdit = null;
    }

    private async Task DeleteRole(RoleDto role)
    {
        await RolesClient.DeleteRoleAsync(role.Id);
        Model!.Roles.Remove(role);
    }
}
