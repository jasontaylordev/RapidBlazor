using CleanArchitecture.Application.AccessControl.Commands;
using CleanArchitecture.Application.AccessControl.Queries;
using CleanArchitecture.WebUI.Shared.AccessControl;
using CleanArchitecture.WebUI.Shared.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Server.Controllers.Admin;

[Route("api/Admin/[controller]")]
public class AccessControlController : ApiControllerBase
{
    [HttpGet]
    [Authorize(Permissions.ViewAccessControl)]
    public async Task<ActionResult<AccessControlVm>> GetConfiguration()
    {
        return await Mediator.Send(new GetAccessControlQuery());
    }

    [HttpPut]
    [Authorize(Permissions.ConfigureAccessControl)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateConfiguration(RoleDto updatedRole)
    {
        await Mediator.Send(new UpdateAccessControlCommand(updatedRole.Id, updatedRole.Permissions));

        return NoContent();
    }
}
