using MediatR;
using Microsoft.AspNetCore.Components;
using RapidBlazor.Application.Users.Queries;
using RapidBlazor.WebUi.Shared.AccessControl;

namespace RapidBlazor.WebUi.Components.Pages.Admin.Users;

public partial class Index
{
    [Inject] private IMediator Mediator { get; set; } = default!;
    
    private UsersVm? Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await Mediator.Send(new GetUsersQuery());
    }
}
