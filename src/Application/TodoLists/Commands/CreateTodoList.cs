using RapidBlazor.WebUI.Shared.TodoLists;

namespace RapidBlazor.Application.TodoLists.Commands;

public record CreateTodoListCommand(CreateTodoListRequest List) : IRequest<int>;

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(p => p.List).SetValidator(new CreateTodoListRequestValidator());

        // Extended validation for server-side.
        RuleFor(p => p.List.Title)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'Title' must be unique.")
                .WithErrorCode("UNIQUE_TITLE");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.TodoLists
            .AllAsync(l => l.Title != title, cancellationToken);
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