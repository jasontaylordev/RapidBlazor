using FluentValidation;

namespace RapidBlazor.WebUi.Shared.TodoItems;

public class CreateTodoItemRequest
{
    public int ListId { get; set; }
    public string Title { get; set; } = string.Empty;
}

public class CreateTodoItemRequestValidator
    : AbstractValidator<CreateTodoItemRequest>
{
    public CreateTodoItemRequestValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(240)
            .NotEmpty();
    }
}
