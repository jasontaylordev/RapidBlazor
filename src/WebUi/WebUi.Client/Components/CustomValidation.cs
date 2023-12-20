using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace RapidBlazor.WebUi.Client.Components;

public class CustomValidation : ComponentBase
{
    [CascadingParameter] private EditContext CurrentEditContext { get; set; } = default!;

    private ValidationMessageStore? _messageStore;

    protected override void OnInitialized()
    {
        if (CurrentEditContext == null)
        {
            throw new InvalidOperationException(
                $"{nameof(CustomValidation)} requires a cascading " +
                $"parameter of type {nameof(EditContext)}. " +
                $"For example, you can use {nameof(CustomValidation)} " +
                $"inside an {nameof(EditForm)}.");
        }

        _messageStore = new(CurrentEditContext);

        CurrentEditContext.OnValidationRequested += (_, _) => _messageStore.Clear();

        CurrentEditContext.OnFieldChanged += (_, e) => _messageStore.Clear(e.FieldIdentifier);
    }

    public void DisplayErrors(IDictionary<string, string[]> errors)
    {
        foreach (var err in errors)
        {
            _messageStore!.Add(CurrentEditContext.Field(err.Key), err.Value);
        }

        CurrentEditContext.NotifyValidationStateChanged();
    }

    public void ClearErrors()
    {
        _messageStore!.Clear();

        CurrentEditContext.NotifyValidationStateChanged();
    }
}
