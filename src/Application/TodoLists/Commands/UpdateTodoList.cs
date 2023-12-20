using FluentValidation;
using RapidBlazor.Application.Common.Services.Data;
using RapidBlazor.WebUi.Shared.TodoLists;

namespace RapidBlazor.Application.TodoLists.Commands;

public sealed record UpdateTodoListCommand(UpdateTodoListRequest List) : IRequest<Unit>;

public sealed class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoListCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        
        // Extended validation for server-side.
        RuleFor(p => p.List.Title)
            .MustAsync(BeUniqueTitle)
            .WithMessage("'Title' must be unique.")
            .WithErrorCode("UNIQUE_TITLE");
    }

    public async Task<bool> BeUniqueTitle(UpdateTodoListCommand model, string title, CancellationToken cancellationToken)
    {
        return await _context.TodoLists
            .Where(l => l.Id != model.List.Id)
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}

public sealed class UpdateTodoListCommandHandler : IRequestHandler<UpdateTodoListCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoLists.FindAsync(request.List.Id);

        Guard.Against.NotFound(request.List.Id, entity);

        entity.Title = request.List.Title;

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
