using Microsoft.AspNetCore.Mvc;
using RapidBlazor.Application.TodoItems.Commands;
using RapidBlazor.WebUi.Shared.Authorization;
using RapidBlazor.WebUi.Shared.TodoItems;

namespace RapidBlazor.WebUi.Controllers;

[Authorize(Permissions.Todo)]
public class TodoItemsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> PostTodoItem(CreateTodoItemRequest request)
    {
        return await Mediator.Send(new CreateTodoItemCommand(request));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutTodoItem(int id, UpdateTodoItemRequest request)
    {
        if (id != request.Id) return BadRequest();

        await Mediator.Send(new UpdateTodoItemCommand(request));

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        await Mediator.Send(new DeleteTodoItemCommand(id));

        return NoContent();
    }
}
