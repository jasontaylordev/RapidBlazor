using RapidBlazor.Application.Common.Services.Identity;
using RapidBlazor.WebUi.Shared.AccessControl;

namespace RapidBlazor.Application.Users.Commands;

public sealed record UpdateUserCommand(UserDto User) : IRequest<Unit>;

public sealed class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IIdentityService _identityService;

    public UpdateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _identityService.UpdateUserAsync(request.User);
        return Unit.Value;
    }
} 
