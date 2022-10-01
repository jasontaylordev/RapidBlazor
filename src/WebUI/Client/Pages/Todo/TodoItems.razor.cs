using CleanArchitecture.WebUI.Shared.TodoItems;
using CleanArchitecture.WebUI.Shared.TodoLists;
using CleanArchitecture.WebUI.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CleanArchitecture.WebUI.Client.Pages.Todo;

public partial class TodoItems
{
    [CascadingParameter]
    public TodoState State { get; set; } = null!;

    public TodoItemDto? SelectedItem { get; set; }

    private ElementReference _titleInput;

    private ElementReference _listOptionsModal;

    public bool IsSelectedItem(TodoItemDto item)
    {
        return SelectedItem == item;

    }

    private async Task AddItem()
    {
        var newItem = new TodoItemDto { ListId = State.SelectedList!.Id };

        State.SelectedList.Items.Add(newItem);

        await EditItem(newItem);
    }

    private async Task ToggleDone(TodoItemDto item, ChangeEventArgs args)
    {
        if (args != null && args.Value is bool value)
        {
            item.Done = value;

            await State.TodoItemsClient.PutTodoItemAsync(item.Id, new UpdateTodoItemRequest
            {
                Id = item.Id,
                ListId = item.ListId,
                Title = item.Title,
                Done = item.Done
            });
        }
    }

    private async Task EditItem(TodoItemDto item)
    {
        SelectedItem = item;

        await Task.Delay(50);

        if (_titleInput.Context != null)
        {
            await _titleInput.FocusAsync();
        }
    }

    private async Task SaveItem()
    {
        if (SelectedItem!.Id == 0)
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.Title))
            {
                State.SelectedList!.Items.Remove(SelectedItem);
            }
            else
            {
                await State.TodoItemsClient.PostTodoItemAsync(new CreateTodoItemRequest
                {
                    ListId = SelectedItem.ListId,
                    Title = SelectedItem.Title
                });
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.Title))
            {
                await State.TodoItemsClient.DeleteTodoItemAsync(SelectedItem.Id);
                State.SelectedList!.Items.Remove(SelectedItem);
            }
            else
            {
                // TODO: Check, is anything else being updated here?
                await State.TodoItemsClient.PutTodoItemAsync(SelectedItem.Id, new UpdateTodoItemRequest
                {
                    Id = SelectedItem.Id,
                    ListId = SelectedItem.ListId,
                    Title = SelectedItem.Title,
                    Done = SelectedItem.Done
                });
            }
        }

        SelectedItem = null;
    }

    private async Task SaveList()
    {
        await State.TodoListsClient.PutTodoListAsync(State.SelectedList!.Id, new UpdateTodoListRequest
        {
            Id = State.SelectedList.Id,
            Title = State.SelectedList.Title
        });

        State.JS.InvokeVoid(JsInteropConstants.HideModal, _listOptionsModal);

        State.SyncList();
    }

    private async Task DeleteList()
    {
        await State.TodoListsClient.DeleteTodoListAsync(State.SelectedList!.Id);

        State.JS.InvokeVoid(JsInteropConstants.HideModal, _listOptionsModal);

        State.DeleteList();
    }
}