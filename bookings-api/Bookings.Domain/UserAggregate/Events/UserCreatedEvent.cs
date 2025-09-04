using Bookings.Domain.Common.Models;

namespace Bookings.Domain.UserAggregate.Events
{
    public record UserCreatedEvent(User User) : IDomainEvent;
}
