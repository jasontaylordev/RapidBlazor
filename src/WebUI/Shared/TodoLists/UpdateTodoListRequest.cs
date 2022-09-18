namespace CleanArchitectureBlazor.WebUI.Shared.TodoLists;

public class UpdateTodoListRequest
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
}
