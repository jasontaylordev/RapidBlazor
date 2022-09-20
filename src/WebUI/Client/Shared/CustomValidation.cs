using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CleanArchitectureBlazor.WebUI.Client.Shared;

public class CustomValidation : ComponentBase
{
    [CascadingParameter]
    private EditContext CurrentEditContext { get; set; } = null!;

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

        // Create a validation message store for the given form
        _messageStore = new(CurrentEditContext);

        // Clear validation errors when validation requested.
        CurrentEditContext.OnValidationRequested += (s, e) =>
            _messageStore.Clear();

        // Clear validation error when field changes.
        CurrentEditContext.OnFieldChanged += (s, e) =>
            _messageStore.Clear(e.FieldIdentifier);
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