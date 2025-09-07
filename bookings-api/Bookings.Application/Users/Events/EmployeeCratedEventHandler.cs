using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.ScheduleAggregate;
using Bookings.Domain.UserAggregate.Events;
using MediatR;

namespace Bookings.Application.Users.Events
{
    public class EmployeeCratedEventHandler(
        IEmployeeRepository employeeRepository,
        IScheduleRepository scheduleRepository) : INotificationHandler<EmployeeCreatedEvent>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IScheduleRepository _scheduleRepository = scheduleRepository;

        public async Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
        {
            var userId = notification.User.Id;

            var existingEmployee = await _employeeRepository.GetByUserIdAsync(userId);
            if (existingEmployee is not null)
                return;

            var employee = Employee.Create(userId);
            _employeeRepository.Add(employee);

            var schedule = Schedule.Create(employee.Id);
            _scheduleRepository.Add(schedule);
        }
    }
}
