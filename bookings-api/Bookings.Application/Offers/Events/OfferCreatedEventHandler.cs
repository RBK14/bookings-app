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

            // Improve error handling
            if (await _employeeRepository.GetByIdAsync(offer.EmployeeId) is not Employee employee)
                throw new Exception($"No employee with EmployeeId: {offer.EmployeeId}.");

            employee.AddOfferId(offer.Id);
            _employeeRepository.Update(employee);
        }
    }
}
