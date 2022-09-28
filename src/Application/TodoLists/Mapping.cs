using CleanArchitecture.WebUI.Shared.TodoLists;

namespace CleanArchitecture.Application.TodoLists;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<TodoList, TodoListDto>();
        CreateMap<TodoItem, TodoItemDto>();
    }
}
