using CleanArchitectureBlazor.Application.TodoItems.Commands;
using CleanArchitectureBlazor.WebUI.Shared.TodoItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureBlazor.WebUI.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/TodoItems
    [HttpPost]
    public async Task<ActionResult<int>> PostTodoItem(
        CreateTodoItemRequest request)
    {
        return await _mediator.Send(new CreateTodoItemCommand(request));
    }

    // PUT: api/TodoItems/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutTodoItem(int id,
        UpdateTodoItemRequest request)
    {
        if (id != request.Id) return BadRequest();

        await _mediator.Send(new UpdateTodoItemCommand(request));

        return NoContent();
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        await _mediator.Send(new DeleteTodoItemCommand(id));

        return NoContent();
    }
}
