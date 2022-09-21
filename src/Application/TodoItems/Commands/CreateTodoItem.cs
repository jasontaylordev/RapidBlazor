using CleanArchitectureBlazor.Domain.Events;
using CleanArchitectureBlazor.WebUI.Shared.TodoItems;

namespace CleanArchitectureBlazor.Application.TodoItems.Commands;

public record CreateTodoItemCommand(CreateTodoItemRequest Item) : IRequest<int>;

public class CreateTodoItemCommandHandler
        : IRequestHandler<CreateTodoItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoItemCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new TodoItem
        {
            ListId = request.Item.ListId,
            Title = request.Item.Title,
            Done = false
        };

        entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        _context.TodoItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}