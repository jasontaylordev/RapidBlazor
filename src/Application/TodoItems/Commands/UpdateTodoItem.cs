using CleanArchitectureBlazor.Domain.Enums;
using CleanArchitectureBlazor.Domain.Events;
using CleanArchitectureBlazor.WebUI.Shared.TodoItems;

namespace CleanArchitectureBlazor.Application.TodoItems.Commands;

public record UpdateTodoItemCommand(UpdateTodoItemRequest Item) : IRequest;

public class UpdateTodoItemCommandHandler
        : AsyncRequestHandler<UpdateTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(UpdateTodoItemCommand request,
            CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems.FirstOrDefaultAsync(
            i => i.Id == request.Item.Id, cancellationToken);

        Guard.Against.NotFound(request.Item.Id, entity);

        entity!.ListId = request.Item.ListId;
        entity.Title = request.Item.Title;
        entity.Done = request.Item.Done;
        entity.Priority = (PriorityLevel)request.Item.Priority;
        entity.Note = request.Item.Note;

        if (entity.Done)
        {
            entity.AddDomainEvent(new TodoItemCompletedEvent(entity));
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}