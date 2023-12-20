using FluentValidation;
using RapidBlazor.Application.Common.Services.Data;
using RapidBlazor.Domain.Entities;
using RapidBlazor.Domain.Events;
using RapidBlazor.WebUi.Shared.TodoItems;

namespace RapidBlazor.Application.TodoItems.Commands;

public sealed record CreateTodoItemCommand(CreateTodoItemRequest Item) : IRequest<int>;

public sealed class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(p => p.Item).SetValidator(new CreateTodoItemRequestValidator());
    }
} 

public sealed class CreateTodoItemCommandHandler
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
