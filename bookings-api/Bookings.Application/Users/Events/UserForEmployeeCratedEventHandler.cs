using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.UserAggregate.Events;
using MediatR;

namespace Bookings.Application.Users.Events
{
    public class UserForEmployeeCratedEventHandler(
        IEmployeeRepository employeeRepository)
        : INotificationHandler<UserForEmployeeCreatedEvent>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task Handle(UserForEmployeeCreatedEvent notification, CancellationToken cancellationToken)
        {
            var userId = notification.User.Id;

            var existingEmployee = await _employeeRepository.GetByUserIdAsync(userId);
            if (existingEmployee is not null)
                return;

            var employee = Employee.Create(userId);
            _employeeRepository.Add(employee);
        }
    }
}