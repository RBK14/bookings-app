using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.AppointmentAggregate;
using MediatR;

namespace Bookings.Application.Appointments.GetAppointments
{
    public class GetAppointmentsQueryHandler(
        IAppointmentRepository appointmentsRepository) : IRequestHandler<GetAppointmentsQuery, IEnumerable<Appointment>>
    {
        private readonly IAppointmentRepository _appointmentsRepository = appointmentsRepository;

        public Task<IEnumerable<Appointment>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
        
