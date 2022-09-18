namespace CleanArchitectureBlazor.WebUI.Shared.TodoItems;

public class CreateTodoItemRequest
{
    public int ListId { get; set; }

    public string Title { get; set; } = string.Empty;
}
