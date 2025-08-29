using Bookings.Domain.AppointmentAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Appointments.Commands.CreateAppointment
{
    public record CreateAppointmentCommand(
        string OfferId,
        string ClientId,
        DateTime StartTime) : IRequest<ErrorOr<Appointment>>;
}
