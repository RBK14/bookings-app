using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.EmployeeAggregate.Events;
using Bookings.Domain.ScheduleAggregate;
using MediatR;

namespace Bookings.Application.Employees.Events
{
    public class EmployeeCreatedEventHandler(
        IScheduleRepository scheduleRepository) 
        : INotificationHandler<EmployeeCreatedEvent>
    {
        private readonly IScheduleRepository _scheduleRepository = scheduleRepository;

        public async Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
        {
            var employeeId = notification.Employee.Id;

            var existingSchedule = await _scheduleRepository.GetByEmployeeIdAsync(employeeId);
            if (existingSchedule is not null)
                return;

            var schedule = Schedule.Create(employeeId);
            _scheduleRepository.Add(schedule);
        }
    }
}
