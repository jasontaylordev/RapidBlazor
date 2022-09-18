using CleanArchitectureBlazor.Application.TodoLists.Commands;
using CleanArchitectureBlazor.Application.TodoLists.Queries;
using CleanArchitectureBlazor.WebUI.Shared.TodoLists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureBlazor.WebUI.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoListsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoListsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/TodoLists
    [HttpGet]
    public async Task<ActionResult<TodosVm>> GetTodoLists()
    {
        return await _mediator.Send(new GetTodoListsQuery());
    }

    // POST: api/TodoLists
    [HttpPost]
    public async Task<ActionResult<int>> PostTodoList(
        CreateTodoListRequest request)
    {
        return await _mediator.Send(new CreateTodoListCommand(request));
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

        await _mediator.Send(new UpdateTodoListCommand(request));

        return NoContent();
    }

    // DELETE: api/TodoLists/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteTodoList(int id)
    {
        await _mediator.Send(new DeleteTodoListCommand(id));

        return NoContent();
    }
}
