using RapidBlazor.Application.Common.Services.Identity;
using RapidBlazor.WebUi.Shared.AccessControl;

namespace RapidBlazor.Application.Roles.Commands;

public sealed record UpdateRoleCommand(RoleDto Role) : IRequest<Unit>;

public sealed class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Unit>
{
    private readonly IIdentityService _identityService;

    public UpdateRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityService.UpdateRoleAsync(request.Role);
        return Unit.Value;
    }
}
