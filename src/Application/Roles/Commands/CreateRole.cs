using RapidBlazor.Application.Common.Services.Identity;
using RapidBlazor.WebUi.Shared.AccessControl;

namespace RapidBlazor.Application.Roles.Commands;

public sealed record CreateRoleCommand(RoleDto Role) : IRequest<Unit>;

public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Unit>
{
    private readonly IIdentityService _identityService;

    public CreateRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityService.CreateRoleAsync(request.Role);
        return Unit.Value;
    }
}
