using Bookings.Domain.Common.Models.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Bookings.Infrastructure.Persistence.Interceptors
{
    public class PublishDomainEventInterceptor(IPublisher mediator) : SaveChangesInterceptor
    {
        private readonly IPublisher _mediator = mediator;

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            PublishBeforeSaveDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await PublishBeforeSaveDomainEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            PublishAfterSaveDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavedChanges(eventData, result);
        }

        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            await PublishAfterSaveDomainEvents(eventData.Context);
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private async Task PublishBeforeSaveDomainEvents(DbContext? dbContext)
        {
            if (dbContext is null)
                return;

            while (true)
            {
                var entitiesWithDomainEvents = dbContext.ChangeTracker
                    .Entries<IHasDomainEvents>()
                    .Where(e => e.Entity.DomainEvents.Any(x => x is IBeforeSaveDomainEvent))
                    .Select(e => e.Entity)
                    .ToList();

                if (entitiesWithDomainEvents.Count == 0)
                    break;

                var domainEvents = entitiesWithDomainEvents
                    .SelectMany(e => e.DomainEvents.OfType<IBeforeSaveDomainEvent>())
                    .ToList();

                entitiesWithDomainEvents.ForEach(e => e.ClearDomainEvents());

                foreach (var domainEvent in domainEvents)
                {
                    await _mediator.Publish(domainEvent);
                }
            }
        }

        private async Task PublishAfterSaveDomainEvents(DbContext? dbContext)
        {
            if (dbContext is null)
                return;

            while (true)
            {
                var entitiesWithDomainEvents = dbContext.ChangeTracker
                    .Entries<IHasDomainEvents>()
                    .Where(e => e.Entity.DomainEvents.Any(x => x is IAfterSaveDomainEvent))
                    .Select(e => e.Entity)
                    .ToList();

                if (entitiesWithDomainEvents.Count == 0)
                    break;

                var domainEvents = entitiesWithDomainEvents
                    .SelectMany(e => e.DomainEvents.OfType<IAfterSaveDomainEvent>())
                    .ToList();

                entitiesWithDomainEvents.ForEach(e => e.ClearDomainEvents());

                foreach (var domainEvent in domainEvents)
                {
                    await _mediator.Publish(domainEvent);
                }
            }
        }
    }
}
