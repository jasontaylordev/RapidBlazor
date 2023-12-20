using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using RapidBlazor.WebUi.Client.Components;
using RapidBlazor.WebUi.Shared.TodoLists;

namespace RapidBlazor.WebUi.Client.Pages.Todo;

public partial class TodoLists
{
    [CascadingParameter] public TodoState State { get; set; } = default!;

    private ElementReference _titleInput;

    private ElementReference _newListModal;

    private TodoListDto _newTodoList = new();

    private CustomValidation? _customValidation;

    private async Task NewList()
    {
        _newTodoList = new TodoListDto();

        await Task.Delay(500);

        if (_titleInput.Context != null)
        {
            await _titleInput.FocusAsync();
        }
    }

    private async Task CreateNewList()
    {
        _customValidation!.ClearErrors();

        try
        {
            var listId = await State.TodoListHandler.PostTodoListAsync(new CreateTodoListRequest
            {
                Title = _newTodoList.Title
            });

            _newTodoList.Id = listId;

            State.Model!.Lists.Add(_newTodoList);

            SelectList(_newTodoList);

            await State.JsModule!.InvokeVoidAsync(JsInteropConstants.HideModal, _newListModal);
        }
        catch (ApiException ex)
        {
            var problemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(ex.Response);

            if (problemDetails is not null)
            {
                var errors = new Dictionary<string, string[]>();

                foreach (var error in problemDetails.Errors)
                {
                    var key = error.Key[(error.Key.IndexOf('.') + 1)..];

                    errors[key] = error.Value;
                }

                _customValidation.DisplayErrors(errors);
            }
        }
    }

    private bool IsSelected(TodoListDto list)
    {
        return State.SelectedList!.Id == list.Id;
    }

    private void SelectList(TodoListDto list)
    {
        if (IsSelected(list)) return;

        State.SelectedList = list;
    }
}
