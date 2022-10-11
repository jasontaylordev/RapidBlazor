using Microsoft.AspNetCore.Mvc;
using RapidBlazor.Application.AccessControl.Commands;
using RapidBlazor.Application.AccessControl.Queries;
using RapidBlazor.WebUI.Shared.AccessControl;
using RapidBlazor.WebUI.Shared.Authorization;

namespace RapidBlazor.WebUI.Server.Controllers.Admin;

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
