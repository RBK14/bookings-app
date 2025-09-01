using ErrorOr;
using MediatR;

namespace Bookings.Application.Appointments.Commands.DeleteAppointment
{
    public record DeleteAppointmentCommand(string AppointmentId) : IRequest<ErrorOr<Unit>>;
}
