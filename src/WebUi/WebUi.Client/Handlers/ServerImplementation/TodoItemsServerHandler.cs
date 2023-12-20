using MediatR;
using RapidBlazor.Application.TodoItems.Commands;
using RapidBlazor.WebUi.Client.Handlers.Interfaces;
using RapidBlazor.WebUi.Shared.TodoItems;

namespace RapidBlazor.WebUi.Client.Handlers.ServerImplementation;

internal class TodoItemsServerHandler :ITodoItemsHandler
{
    private readonly IMediator _mediator;

    public TodoItemsServerHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task PutTodoItemAsync(int id, UpdateTodoItemRequest request)
    {
        return _mediator.Send(new UpdateTodoItemCommand(request));
    }

    public Task<int> PostTodoItemAsync(CreateTodoItemRequest request)
    {
        return _mediator.Send(new CreateTodoItemCommand(request));
    }

    public Task DeleteTodoItemAsync(int id)
    {
        return _mediator.Send(new DeleteTodoItemCommand(id));
    }
}
