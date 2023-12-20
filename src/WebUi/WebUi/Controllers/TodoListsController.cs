using Microsoft.AspNetCore.Mvc;
using RapidBlazor.Application.TodoLists.Commands;
using RapidBlazor.Application.TodoLists.Queries;
using RapidBlazor.WebUi.Shared.Authorization;
using RapidBlazor.WebUi.Shared.TodoLists;

namespace RapidBlazor.WebUi.Controllers;

[Authorize(Permissions.Todo)]
public class TodoListsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<TodosVm>> GetTodoLists()
    {
        return await Mediator.Send(new GetTodoListsQuery());
    }

    // POST: api/TodoLists
    [HttpPost]
    public async Task<ActionResult<int>> PostTodoList(
        CreateTodoListRequest request)
    {
        return await Mediator.Send(new CreateTodoListCommand(request));
    }

    // PUT: api/TodoLists/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutTodoList(int id,
        UpdateTodoListRequest request)
    {
        if (id != request.Id) return BadRequest();

        await Mediator.Send(new UpdateTodoListCommand(request));

        return NoContent();
    }

    // DELETE: api/TodoLists/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteTodoList(int id)
    {
        await Mediator.Send(new DeleteTodoListCommand(id));

        return NoContent();
    }
}
