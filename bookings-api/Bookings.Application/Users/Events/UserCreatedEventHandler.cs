using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.UserAggregate.Enums;
using Bookings.Domain.UserAggregate.Events;
using MediatR;

namespace Bookings.Application.Users.Events
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public UserCreatedEventHandler(IClientRepository clientRepository, IEmployeeRepository employeeRepository)
        {
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var userId = notification.user.Id;
            var userRole = notification.user.Role;

            if (userRole == UserRole.Client)
            {
                var client = Client.Create(userId);
                await _clientRepository.AddAsync(client);
            }
            else if (userRole == UserRole.Employee)
            {
                var employee = Employee.Create(userId);
                await _employeeRepository.AddAsync(employee);
            }
        }
    }
}
