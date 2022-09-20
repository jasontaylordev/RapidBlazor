using CleanArchitectureBlazor.WebUI.Shared.AccessControl;
using Microsoft.AspNetCore.Components;

namespace CleanArchitectureBlazor.WebUI.Client.Pages.Admin.Users;

public partial class Index
{
    [Inject]
    public IUsersClient UsersClient { get; set; } = null!;

    public UsersVm? Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await UsersClient.GetUsersAsync();
    }
}
