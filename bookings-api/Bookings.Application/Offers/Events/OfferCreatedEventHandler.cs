using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.OfferAggregate.Events;
using MediatR;

namespace Bookings.Application.Offers.Events
{
    public class OfferCreatedEventHandler(IEmployeeRepository employeeRepository) : INotificationHandler<OfferCreatedEvent>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task Handle(OfferCreatedEvent notification, CancellationToken cancellationToken)
        {
            var offer = notification.Offer;

            if (await _employeeRepository.GetByIdAsync(offer.EmployeeId) is not Employee employee)
                throw new Exception($"No employee with EmployeeId: {offer.EmployeeId}."); // TODO: Ogarnąć lepszy error handling

            employee.AddOfferId(offer.Id);
            await _employeeRepository.UpdateAsync(employee);
        }
    }
}
