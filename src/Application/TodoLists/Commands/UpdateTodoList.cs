using CleanArchitectureBlazor.WebUI.Shared.TodoLists;

namespace CleanArchitectureBlazor.Application.TodoLists.Commands;

public record UpdateTodoListCommand(UpdateTodoListRequest List) : IRequest;

public class UpdateTodoListCommandHandler
    : AsyncRequestHandler<UpdateTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    protected override async Task Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoLists.FindAsync(request.List.Id);

        Guard.Against.NotFound(request.List.Id, entity);

        entity.Title = request.List.Title;

        await _context.SaveChangesAsync(cancellationToken);
    }
}