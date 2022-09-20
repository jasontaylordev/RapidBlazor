using CleanArchitectureBlazor.Application.Common.Services.Identity;
using CleanArchitectureBlazor.WebUI.Shared.AccessControl;

namespace CleanArchitectureBlazor.Application.Roles.Queries;

public record GetRolesQuery : IRequest<RolesVm>;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, RolesVm>
{
    private readonly IIdentityService _identityService;

    public GetRolesQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<RolesVm> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        return new RolesVm
        {
            Roles = await _identityService.GetRolesAsync(cancellationToken)
        };
    }
}
