using RapidBlazor.Domain.Common;
using RapidBlazor.Domain.Entities;

namespace RapidBlazor.Domain.Events;

public class TodoItemCreatedEvent : BaseEvent
{
    public TodoItemCreatedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
