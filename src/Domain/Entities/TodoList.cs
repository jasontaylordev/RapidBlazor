using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities;

public class TodoList : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;

    public string Colour { get; set; } = string.Empty;

    public ICollection<TodoItem> Items { get; set; }
        = new List<TodoItem>();
}
