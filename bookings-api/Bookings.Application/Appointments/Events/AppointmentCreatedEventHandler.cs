using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.AppointmentAggregate.Events;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.OfferAggregate;
using MediatR;

namespace Bookings.Application.Appointments.Events
{
    public class AppointmentCreatedEventHandler(
        IOfferRepository offerRepository,
        IClientRepository clientRepository,
        IEmployeeRepository employeeRepository) : INotificationHandler<AppointmentCreatedEvent>
    {
        private readonly IOfferRepository _offerRepository = offerRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IClientRepository _clientRepository = clientRepository;

        public async Task Handle(AppointmentCreatedEvent notification, CancellationToken cancellationToken)
        {
            var appointment = notification.Appointment;

            if (await _offerRepository.GetByIdAsync(appointment.OfferId) is not Offer offer)
                throw new Exception($"No offer with OfferId: {appointment.OfferId}."); // TODO: Ogarnąć lepszy error handling

            if (await _employeeRepository.GetByIdAsync(appointment.EmployeeId) is not Employee employee)
                throw new Exception($"No employee with EmployeeId: {appointment.EmployeeId}.");

            if (await _clientRepository.GetByIdAsync(appointment.ClientId) is not Client client)
                throw new Exception($"No client with ClientId: {appointment.ClientId}.");

            offer.AddAppointmentId(appointment.Id);
            await _offerRepository.UpdateAsync(offer);

            employee.AddAppointmentId(appointment.Id);
            await _employeeRepository.UpdateAsync(employee);

            client.AddAppointmentId(appointment.Id);
            await _clientRepository.UpdateAsync(client);
        }
    }
}
