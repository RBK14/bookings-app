using Bookings.Domain.Common.Models.Events;

namespace Bookings.Domain.AppointmentAggregate.Events
{
    public record AppointmentCreatedEvent(Appointment Appointment) : IBeforeSaveDomainEvent;
}
