using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public static class MediatorExtensions
{
    public static async Task DispatchDomainEvents(this IMediator mediator, DbContext context)
    {
        var entities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(entityEntry => entityEntry.Entity.DomainEvents.Any())
            .Select(entityEntry => entityEntry.Entity);

        var domainEvents = entities
            .SelectMany(baseEntity => baseEntity.DomainEvents)
            .ToList();

        entities.ToList().ForEach(basenEntity => basenEntity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent).ConfigureAwait(false);
        }
    }
}
