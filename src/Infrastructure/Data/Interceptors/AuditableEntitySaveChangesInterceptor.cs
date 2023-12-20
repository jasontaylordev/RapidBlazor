using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RapidBlazor.Application.Common.Services.Data;
using RapidBlazor.Application.Common.Services.DateTime;
using RapidBlazor.Application.Common.Services.Identity;
using RapidBlazor.Domain.Common;

namespace RapidBlazor.Infrastructure.Data.Interceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUser _currentUser;
    private readonly IDateTimeProvider _dateTime;

    public AuditableEntitySaveChangesInterceptor(ICurrentUser currentUser, 
        IDateTimeProvider dateTime)
    {
        _currentUser = currentUser;
        _dateTime = dateTime;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        AuditEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        AuditEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void AuditEntities(DbContext? context)
    {
        if (context is null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _currentUser.UserId;
                entry.Entity.CreatedUtc = _dateTime.UtcNow;
            }
            
            if (entry.State == EntityState.Added ||
                entry.State == EntityState.Modified)
            {
                entry.Entity.LastModifiedBy = _currentUser.UserId;
                entry.Entity.LastModifiedUtc = _dateTime.UtcNow;
            }
        }
    }
}
