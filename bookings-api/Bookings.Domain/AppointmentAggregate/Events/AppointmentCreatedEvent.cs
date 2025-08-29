using Bookings.Domain.Common.Models;

namespace Bookings.Domain.AppointmentAggregate.Events
{
    public record AppointmentCreatedEvent(Appointment Appointment) : IDomainEvent;
}
