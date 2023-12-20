using MediatR;
using RapidBlazor.Application.Users.Commands;
using RapidBlazor.Application.Users.Queries;
using RapidBlazor.WebUi.Client.Handlers.Interfaces;
using RapidBlazor.WebUi.Shared.AccessControl;

namespace RapidBlazor.WebUi.Client.Handlers.ServerImplementation;

internal class UserServerHandler : IUserHandler
{
    private readonly IMediator _mediator;

    public UserServerHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<UserDetailsVm> GetUserAsync(string userId)
    {
        return _mediator.Send(new GetUserQuery(userId));
    }

    public Task PutUserAsync(string userId, UserDto user)
    {
        return _mediator.Send(new UpdateUserCommand(user));
    }
}
