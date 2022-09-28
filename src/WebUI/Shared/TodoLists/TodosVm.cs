using CleanArchitecture.WebUI.Shared.Common;

namespace CleanArchitecture.WebUI.Shared.TodoLists;

public class TodosVm
{
    public List<LookupDto> PriorityLevels { get; set; } = new();

    public List<TodoListDto> Lists { get; set; } = new();
}
