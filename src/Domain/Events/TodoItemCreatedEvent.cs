using RapidBlazor.Domain.Common;
using RapidBlazor.Domain.Entities;

namespace RapidBlazor.Domain.Events;

public sealed class TodoItemCreatedEvent : BaseEvent
{
    public TodoItem Item { get; }

    public TodoItemCreatedEvent(TodoItem item)
    {
        Item = item;
    }
}
