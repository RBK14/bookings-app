using Bookings.Domain.Common.Models;

namespace Bookings.Domain.UserAggregate.Events
{
    public record EmployeeCreatedEvent(User User) : IDomainEvent;
}
