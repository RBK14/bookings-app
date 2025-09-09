using Bookings.Domain.Common.Models.Events;

namespace Bookings.Domain.UserAggregate.Events
{
    public record UserForClientCreatedEvent(User User) : IBeforeSaveDomainEvent;
}
