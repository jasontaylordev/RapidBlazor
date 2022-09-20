using CleanArchitectureBlazor.Application.Roles.Commands;
using CleanArchitectureBlazor.Application.Roles.Queries;
using CleanArchitectureBlazor.WebUI.Shared.AccessControl;
using CleanArchitectureBlazor.WebUI.Shared.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureBlazor.WebUI.Server.Controllers.Admin;

[Route("api/Admin/[controller]")]
public class RolesController : ApiControllerBase
{
    // GET: api/Admin/Roles
    [HttpGet]
    [Authorize(Permissions.ViewRoles)]
    public async Task<ActionResult<RolesVm>> GetRoles()
    {
        return await Mediator.Send(new GetRolesQuery());
    }

    // POST: api/Admin/Roles
    [HttpPost]
    [Authorize(Permissions.ManageRoles)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PostRole(RoleDto newRole)
    {
        await Mediator.Send(new CreateRoleCommand(newRole));
        
        return NoContent();
    }

    // PUT: api/Admin/Roles/5
    [HttpPut("{id}")]
    [Authorize(Permissions.ManageRoles)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutRole(string id, RoleDto updatedRole)
    {
        if (id != updatedRole.Id) return BadRequest();

        await Mediator.Send(new UpdateRoleCommand(updatedRole));

        return NoContent();
    }

    // DELETE: api/Admin/Roles/5
    [HttpDelete("{id}")]
    [Authorize(Permissions.ManageRoles)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRole(string id)
    {
        await Mediator.Send(new DeleteRoleCommand(id));

        return NoContent();
    }
}
