using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RapidBlazor.WebUi.Client.Handlers;
using RapidBlazor.WebUi.Client.Handlers.Interfaces;
using RapidBlazor.WebUi.Shared.TodoLists;

namespace RapidBlazor.WebUi.Client.Pages.Todo;

public partial class TodoState
{
    [Parameter] public RenderFragment ChildContent { get; set; } = default!;

    [Inject] public ITodoListHandler TodoListHandler { get; set; } = default!;

    [Inject] public ITodoItemsHandler TodoItemsHandler { get; set; } = default!;

    [Inject] private IJSRuntime JS { get; set; } = default!;

    public TodosVm? Model { get; set; }

    private TodoListDto? _selectedList;
    
    public bool Initialized { get; private set; }

    public TodoListDto? SelectedList
    {
        get => _selectedList;
        set
        {
            _selectedList = value;
            StateHasChanged();
        }
    }

    public IJSObjectReference? JsModule { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await TodoListHandler.GetTodoListsAsync();
        SelectedList = Model.Lists.First();
        Initialized = true;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            JsModule = await JS.InvokeAsync<IJSObjectReference>(
                "import", "./Pages/Todo/TodoState.razor.js");
        }
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
