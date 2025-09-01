using Bookings.Domain.AppointmentAggregate;
using MediatR;

namespace Bookings.Application.Appointments.GetAppointments
{
    public record GetAppointmentsQuery() : IRequest<IEnumerable<Appointment>>;
}
