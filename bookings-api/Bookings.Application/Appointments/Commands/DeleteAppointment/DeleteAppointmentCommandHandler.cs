using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Appointments.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommandHandler(IAppointmentRepository appointmentsRepository) : IRequestHandler<DeleteAppointmentCommand, ErrorOr<Unit>>
    {
        private readonly IAppointmentRepository _appointmentsRepository = appointmentsRepository;
        public async Task<ErrorOr<Unit>> Handle(DeleteAppointmentCommand command, CancellationToken cancellationToken)
        {
            var appointmentId = AppointmentId.Create(command.AppointmentId);

            if (await _appointmentsRepository.GetByIdAsync(appointmentId) is not Appointment appointment)
                return Errors.Appointment.NotFound;

            _appointmentsRepository.Update(appointment);
            await _appointmentsRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
