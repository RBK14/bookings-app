using Bookings.Domain.AppointmentAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Appointments.Commands.UpdateAppointment
{
    public record UpdateAppointmentCommand(
        string AppointmentId,
        string EmployeeId,
        DateTime StartTime,
        DateTime EndTime) : IRequest<ErrorOr<Appointment>>;
}
