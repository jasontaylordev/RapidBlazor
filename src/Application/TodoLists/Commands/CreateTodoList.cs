using FluentValidation;
using RapidBlazor.Application.Common.Services.Data;
using RapidBlazor.Domain.Entities;
using RapidBlazor.WebUi.Shared.TodoLists;

namespace RapidBlazor.Application.TodoLists.Commands;

public sealed record CreateTodoListCommand(CreateTodoListRequest List) : IRequest<int>;

public sealed class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateTodoListCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(p => p.List).SetValidator(new CreateTodoListRequestValidator());
        RuleFor(p=>p.List.Title)
            .MustAsync(BeUniqueTitle)
            .WithMessage("'Title' must be unique.")
            .WithErrorCode("UNIQUE_TITLE");
    }

    private Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return _context.TodoLists
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}

public sealed class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList();

        entity.Title = request.List.Title;

        _context.TodoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
