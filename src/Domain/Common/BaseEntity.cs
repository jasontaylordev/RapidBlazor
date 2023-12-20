using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidBlazor.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }

    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped] public ImmutableList<BaseEvent> DomainEvents => ImmutableList.Create(_domainEvents.ToArray());

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
