using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.UserAggregate.Enums;
using Bookings.Domain.UserAggregate.Events;
using MediatR;

namespace Bookings.Application.Users.Events
{
    public class UserCreatedEventHandler(
        IClientRepository clientRepository,
        IEmployeeRepository employeeRepository) : INotificationHandler<UserCreatedEvent>
    {
        private readonly IClientRepository _clientRepository = clientRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var userId = notification.User.Id;
            var userRole = notification.User.Role;

            if (userRole == UserRole.Client)
            {
                var client = Client.Create(userId);
                _clientRepository.Add(client);
            }
            else if (userRole == UserRole.Employee)
            {
                var employee = Employee.Create(userId);
                _employeeRepository.Add(employee);
            }

            return Task.CompletedTask;
        }
    }
}
