using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.AppointmentAggregate.Events;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.OfferAggregate;
using Bookings.Domain.ScheduleAggregate;
using MediatR;

namespace Bookings.Application.Appointments.Events
{
    public class AppointmentCreatedEventHandler(
        IOfferRepository offerRepository,
        IClientRepository clientRepository,
        IEmployeeRepository employeeRepository,
        IScheduleRepository scheduleRepository) : INotificationHandler<AppointmentCreatedEvent>
    {
        private readonly IOfferRepository _offerRepository = offerRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IScheduleRepository _scheduleRepository = scheduleRepository;

        public async Task Handle(AppointmentCreatedEvent notification, CancellationToken cancellationToken)
        {
            var appointment = notification.Appointment;

            // TODO: Improve error handling
            if (await _offerRepository.GetByIdAsync(appointment.OfferId) is not Offer offer)
                throw new Exception($"No offer with OfferId: {appointment.OfferId}.");

            if (await _employeeRepository.GetByIdAsync(appointment.EmployeeId) is not Employee employee)
                throw new Exception($"No employee with EmployeeId: {appointment.EmployeeId}.");

            if (await _clientRepository.GetByIdAsync(appointment.ClientId) is not Client client)
                throw new Exception($"No client with ClientId: {appointment.ClientId}.");

            if (await _scheduleRepository.GetByEmployeeIdAsync(appointment.EmployeeId) is not Schedule schedule)
                throw new Exception("$No client with ClientId: {appointment.ClientId}.");

            offer.AddAppointmentId(appointment.Id);
            _offerRepository.Update(offer);

            employee.AddAppointmentId(appointment.Id);
            _employeeRepository.Update(employee);

            client.AddAppointmentId(appointment.Id);
            _clientRepository.Update(client);

            schedule.BookAppointmentSlot(appointment.Id, appointment.Time.Start, appointment.Time.End);
            _scheduleRepository.Update(schedule);
        }
    }
}
