using Bookings.Domain.Common.Models.Events;

namespace Bookings.Domain.UserAggregate.Events
{
    public record UserForEmployeeCreatedEvent(User User) : IBeforeSaveDomainEvent;
}
