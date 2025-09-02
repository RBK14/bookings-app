using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler(IAppointmentRepository appointmentRepository) : IRequestHandler<UpdateAppointmentCommand, ErrorOr<Appointment>>
    {
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;

        public async Task<ErrorOr<Appointment>> Handle(UpdateAppointmentCommand command, CancellationToken cancellationToken)
        {
            var appointmentId = AppointmentId.Create(command.AppointmentId);

            if (await _appointmentRepository.GetByIdAsync(appointmentId) is not Appointment appointment)
                return Errors.Appointment.NotFound;

            var employeeId = EmployeeId.Create(command.EmployeeId);

            bool isAdmin = employeeId.Value == Guid.Empty;

            if (!isAdmin && appointment.EmployeeId != employeeId)
                return Errors.User.NoPermissions;

            appointment.UpdateTime(command.StartTime, command.EndTime);

            await _appointmentRepository.UpdateAsync(appointment);

            return appointment;
        }
    }
}
