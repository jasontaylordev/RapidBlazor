using RapidBlazor.WebUi.Shared.Common;

namespace RapidBlazor.WebUi.Shared.TodoLists;

public class TodosVm
{
    public List<LookupDto> PriorityLevels { get; set; } = new();

    public List<TodoListDto> Lists { get; set; } = new();
}
