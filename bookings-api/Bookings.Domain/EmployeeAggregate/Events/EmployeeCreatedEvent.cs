using Bookings.Domain.Common.Models;

namespace Bookings.Domain.EmployeeAggregate.Events
{
    public record EmployeeCreatedEvent(Employee Employee) : IDomainEvent;
}
