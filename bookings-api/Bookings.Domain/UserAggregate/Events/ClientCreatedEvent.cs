using Bookings.Domain.Common.Models;

namespace Bookings.Domain.UserAggregate.Events
{
    public record ClientCreatedEvent(User User) : IDomainEvent;
}
