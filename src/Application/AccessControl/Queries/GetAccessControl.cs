using RapidBlazor.Application.Common.Services.Identity;
using RapidBlazor.WebUI.Shared.AccessControl;
using RapidBlazor.WebUI.Shared.Authorization;

namespace RapidBlazor.Application.AccessControl.Queries;

public record GetAccessControlQuery() : IRequest<AccessControlVm>;

public class GetAccessControlQueryHandler : IRequestHandler<GetAccessControlQuery, AccessControlVm>
{
    private readonly IIdentityService _identityService;

    public GetAccessControlQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AccessControlVm> Handle(GetAccessControlQuery request, CancellationToken cancellationToken)
    {
        var permissions = new List<Permissions>();
        foreach (var permission in PermissionsProvider.GetAll())
        {
            if (permission == Permissions.None) continue;

            permissions.Add(permission);
        }

        var roles = await _identityService.GetRolesAsync(cancellationToken);

        var result = new AccessControlVm(roles, permissions);

        return result;
    }
}
