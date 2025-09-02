using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Appointments.Queries.GetAppointment
{
    public class GetAppointmentQueryHandler(
        IAppointmentRepository appointmentRepository) : IRequestHandler<GetAppointmentQuery, ErrorOr<Appointment>>
    {
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;

        public async Task<ErrorOr<Appointment>> Handle(GetAppointmentQuery query, CancellationToken cancellationToken)
        {
            var appointmentId = AppointmentId.Create(query.AppointmentId);

            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);

            if (appointment is null)
                return Errors.Appointment.NotFound;

            return appointment;
        }
    }
}
