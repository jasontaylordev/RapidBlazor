using RapidBlazor.Application.Common.Services.Identity;

namespace RapidBlazor.Application.Roles.Commands;

public sealed record DeleteRoleCommand(string RoleId) : IRequest<Unit>;

public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
{
    private readonly IIdentityService _identityService;

    public DeleteRoleCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityService.DeleteRoleAsync(request.RoleId);
        return Unit.Value;
    }
}
