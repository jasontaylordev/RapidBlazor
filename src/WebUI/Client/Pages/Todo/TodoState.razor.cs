using RapidBlazor.WebUI.Shared.TodoLists;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace RapidBlazor.WebUI.Client.Pages.Todo;

public partial class TodoState
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    [Inject]
    public ITodoListsClient TodoListsClient { get; set; } = null!;

    [Inject] 
    public ITodoItemsClient TodoItemsClient { get; set; } = null!;

    [Inject] 
    public IJSInProcessRuntime JS { get; set; } = null!;

    public TodosVm? Model { get; set; }

    private TodoListDto? _selectedList;

    public TodoListDto? SelectedList
    {
        get { return _selectedList; }
        set
        {
            _selectedList = value;
            StateHasChanged();
        }
    }

    public bool Initialised { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await TodoListsClient.GetTodoListsAsync();
        SelectedList = Model.Lists.First();
        Initialised = true;
    }

    public void SyncList()
    {
        var list = Model!.Lists.First(l => l.Id == SelectedList!.Id);

        list.Title = SelectedList!.Title;

        StateHasChanged();
    }

    public void DeleteList()
    {
        var list = Model!.Lists.First(l => l.Id == SelectedList!.Id);

        Model!.Lists.Remove(list);

        SelectedList = Model.Lists.First();

        StateHasChanged();
    }
}