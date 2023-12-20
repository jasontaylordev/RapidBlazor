using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RapidBlazor.WebUi.Shared.TodoItems;
using RapidBlazor.WebUi.Shared.TodoLists;

namespace RapidBlazor.WebUi.Client.Pages.Todo;

public partial class TodoItems
{
    [CascadingParameter] public TodoState State { get; set; } = default!;
    
    private TodoItemDto? SelectedItem { get; set; }
    
    private ElementReference _titleInput;

    private ElementReference _listOptionsModal;

    private bool IsSelectedItem(TodoItemDto item)
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
        if (args.Value is bool value)
        {
            item.Done = value;

            await State.TodoItemsHandler.PutTodoItemAsync(item.Id,
                new UpdateTodoItemRequest { Id = item.Id, ListId = item.ListId, Title = item.Title, Done = item.Done });
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
                var itemId = await State.TodoItemsHandler.PostTodoItemAsync(new CreateTodoItemRequest
                {
                    ListId = SelectedItem.ListId, Title = SelectedItem.Title
                });

                SelectedItem.Id = itemId;
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(SelectedItem.Title))
            {
                await State.TodoItemsHandler.DeleteTodoItemAsync(SelectedItem.Id);
                State.SelectedList!.Items.Remove(SelectedItem);
            }
            else
            {
                // TODO: Check, is anything else being updated here?
                await State.TodoItemsHandler.PutTodoItemAsync(SelectedItem.Id,
                    new UpdateTodoItemRequest
                    {
                        Id = SelectedItem.Id,
                        ListId = SelectedItem.ListId,
                        Title = SelectedItem.Title,
                        Done = SelectedItem.Done
                    });
            }
        }
    }

    private async Task SaveList()
    {
        await State.TodoListHandler.PutTodoListAsync(State.SelectedList!.Id, new UpdateTodoListRequest
        {
            Id = State.SelectedList.Id,
            Title = State.SelectedList.Title
        });

        await State.JsModule!.InvokeVoidAsync(JsInteropConstants.HideModal, _listOptionsModal);

        State.SyncList();
    }

    private async Task DeleteList()
    {
        await State.TodoListHandler.DeleteTodoListAsync(State.SelectedList!.Id);

        await State.JsModule!.InvokeVoidAsync(JsInteropConstants.HideModal, _listOptionsModal);

        State.DeleteList();
    }
}
