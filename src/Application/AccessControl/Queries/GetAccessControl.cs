using CleanArchitectureBlazor.Application.Common.Services.Identity;
using CleanArchitectureBlazor.WebUI.Shared.AccessControl;

namespace CleanArchitectureBlazor.Application.AccessControl.Queries;

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
        var result = new AccessControlVm
        {
            Roles = await _identityService.GetRolesAsync(cancellationToken)
        };

        return result;
    }
}
