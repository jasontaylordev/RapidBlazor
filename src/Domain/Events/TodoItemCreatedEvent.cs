using CleanArchitectureBlazor.Domain.Common;
using CleanArchitectureBlazor.Domain.Entities;

namespace CleanArchitectureBlazor.Domain.Events;

public class TodoItemCreatedEvent : BaseEvent
{
    public TodoItemCreatedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
