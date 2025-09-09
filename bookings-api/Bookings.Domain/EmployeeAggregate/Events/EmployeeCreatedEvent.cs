using Bookings.Domain.Common.Models.Events;

namespace Bookings.Domain.EmployeeAggregate.Events
{
    public record EmployeeCreatedEvent(Employee Employee) : IBeforeSaveDomainEvent;
}
