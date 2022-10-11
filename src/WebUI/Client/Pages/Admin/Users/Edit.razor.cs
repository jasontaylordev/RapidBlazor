using Microsoft.AspNetCore.Components;
using RapidBlazor.WebUI.Shared.AccessControl;

namespace RapidBlazor.WebUI.Client.Pages.Admin.Users;

public partial class Edit
{
    [Parameter]
    public string UserId { get; set; } = null!;

    [Inject]
    public IUsersClient UsersClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    public UserDetailsVm? Model { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        Model = await UsersClient.GetUserAsync(UserId);
    }

    public void ToggleSelectedRole(string roleName)
    {
        if (Model!.User.Roles.Contains(roleName))
        {
            Model.User.Roles.Remove(roleName);
        }
        else
        {
            Model.User.Roles.Add(roleName);
        }

        StateHasChanged();
    }

    public async Task UpdateUser()
    {
        await UsersClient.PutUserAsync(Model!.User.Id, Model.User);

        Navigation.NavigateTo("/admin/users");
    }
}
