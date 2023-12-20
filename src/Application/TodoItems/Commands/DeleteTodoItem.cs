using RapidBlazor.Application.Common.Services.Data;

namespace RapidBlazor.Application.TodoItems.Commands;

public sealed record DeleteTodoItemCommand(int Id) : IRequest<Unit>;

public sealed class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems.FindAsync(request.Id);

        Guard.Against.NotFound(request.Id, entity);

        _context.TodoItems.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
