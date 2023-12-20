using RapidBlazor.Application.Common.Services.Data;

namespace RapidBlazor.Application.TodoLists.Commands;

public sealed record DeleteTodoListCommand(int Id) : IRequest<Unit>;

public sealed class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTodoListCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = await _context.TodoLists
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.TodoLists.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
