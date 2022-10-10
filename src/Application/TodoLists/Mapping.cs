using RapidBlazor.WebUI.Shared.TodoLists;

namespace RapidBlazor.Application.TodoLists;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<TodoList, TodoListDto>();
        CreateMap<TodoItem, TodoItemDto>();
    }
}
