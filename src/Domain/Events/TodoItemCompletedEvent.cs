using RapidBlazor.Domain.Common;
using RapidBlazor.Domain.Entities;

namespace RapidBlazor.Domain.Events;

public sealed class TodoItemCompletedEvent : BaseEvent
{
    public TodoItem Item { get; }

    public TodoItemCompletedEvent(TodoItem item)
    {
        Item = item;
    }
}
