using CleanArchitectureBlazor.WebUI.Shared.TodoLists;

namespace CleanArchitectureBlazor.Application.TodoLists.Commands;

public record CreateTodoListCommand(CreateTodoListRequest List) : IRequest<int>;

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    public CreateTodoListCommandValidator()
    {
        RuleFor(p => p.List).SetValidator(new CreateTodoListRequestValidator());
    }
}

public class CreateTodoListCommandHandler
    : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoListCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new TodoList();

        entity.Title = request.List.Title;

        _context.TodoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}