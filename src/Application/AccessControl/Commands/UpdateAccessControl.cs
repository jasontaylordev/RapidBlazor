using CleanArchitectureBlazor.Application.Common.Services.Identity;
using CleanArchitectureBlazor.WebUI.Shared.Authorization;

namespace CleanArchitectureBlazor.Application.AccessControl.Commands;

public record UpdateAccessControlCommand(string RoleId, Permissions Permissions): IRequest;

public class UpdateAccessControlCommandHandler : AsyncRequestHandler<UpdateAccessControlCommand>
{
    private readonly IIdentityService _identityService;

    public UpdateAccessControlCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    protected override async Task Handle(UpdateAccessControlCommand request, CancellationToken cancellationToken)
    {
        await _identityService.UpdateRolePermissionsAsync(request.RoleId, request.Permissions);
    }
}
