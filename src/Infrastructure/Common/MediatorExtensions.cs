using MediatR;
using Microsoft.EntityFrameworkCore;
using RapidBlazor.Domain.Common;

namespace RapidBlazor.Infrastructure.Common;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEvent(this IPublisher mediator, DbContext context)
    {
        var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => !e.Entity.DomainEvents.IsEmpty)
            .Select(e => e.Entity)
            .ToList();

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();
        
        entities.ForEach(e=>e.ClearDomainEvents());
        
        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}
