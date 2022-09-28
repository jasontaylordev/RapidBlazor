using FluentValidation;

namespace CleanArchitecture.WebUI.Shared.TodoLists;

public class CreateTodoListRequest
{
    public string Title { get; set; } = string.Empty;
}

public class CreateTodoListRequestValidator
    : AbstractValidator<CreateTodoListRequest>
{
    public CreateTodoListRequestValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(240)
            .NotEmpty();
    }
}
