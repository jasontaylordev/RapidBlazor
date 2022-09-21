using CleanArchitectureBlazor.Domain.Common;
using CleanArchitectureBlazor.Domain.Enums;

namespace CleanArchitectureBlazor.Domain.Entities;

public class TodoItem : BaseAuditableEntity
{
    public int ListId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Note { get; set; } = string.Empty;

    public bool Done { get; set; }

    public DateTime? Reminder { get; set; }

    public PriorityLevel Priority { get; set; }

    public TodoList List { get; set; } = null!;
}
