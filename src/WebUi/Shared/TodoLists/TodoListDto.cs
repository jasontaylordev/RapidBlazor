namespace RapidBlazor.WebUi.Shared.TodoLists;

public class TodoListDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Colour { get; set; } = string.Empty;

    public List<TodoItemDto> Items { get; set; } = new();
}
