using Bookings.Domain.Common.Models.Events;

namespace Bookings.Domain.VerificationTokenAggregate.Events
{
    public record VerificationTokenCreatedEvent(VerificationToken Token) : IAfterSaveDomainEvent;
}
