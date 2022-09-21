using CleanArchitectureBlazor.Domain.Common;
using CleanArchitectureBlazor.Domain.Entities;

namespace CleanArchitectureBlazor.Domain.Events;

public class TodoItemCompletedEvent : BaseEvent
{
    public TodoItemCompletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
