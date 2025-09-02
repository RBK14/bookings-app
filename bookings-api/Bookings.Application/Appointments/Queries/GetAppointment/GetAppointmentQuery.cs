using Bookings.Domain.AppointmentAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Appointments.Queries.GetAppointment
{
    public record GetAppointmentQuery(string AppointmentId) : IRequest<ErrorOr<Appointment>>;
}
