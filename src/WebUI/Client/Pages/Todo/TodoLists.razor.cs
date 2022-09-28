using CleanArchitecture.WebUI.Client.Shared;
using CleanArchitecture.WebUI.Shared.TodoLists;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace CleanArchitecture.WebUI.Client.Pages.Todo;

public partial class TodoLists
{
    [CascadingParameter]
    public TodoState State { get; set; } = null!;

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
            var listId = await State.TodoListsClient.PostTodoListAsync(new CreateTodoListRequest
            {
                Title = _newTodoList.Title
            });

            _newTodoList.Id = listId;

            State.Model!.Lists.Add(_newTodoList);

            SelectList(_newTodoList);

            State.JS.InvokeVoid(JsInteropConstants.HideModal, _newListModal);
        }
        catch (ApiException ex)
        {
            // NOTE: This demonstrates handling of validation errors that came back from the server side.
            var problemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(ex.Response);

            if (problemDetails != null)
            {
                // The problemDetails.Errors dictionary contains keys prefixed with "List.".
                // The prefix must be removed for errors to display correctly.
                var errors = new Dictionary<string, string[]>();

                foreach (var error in problemDetails.Errors)
                {
                    var key = error.Key[(error.Key.IndexOf(".") + 1)..];

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