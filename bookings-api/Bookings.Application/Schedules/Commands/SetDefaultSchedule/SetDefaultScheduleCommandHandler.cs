using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.ScheduleAggregate;
using Bookings.Domain.ScheduleAggregate.Entities;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Schedules.Commands.SetDefaultSchedule
{
    public class SetDefaultScheduleCommandHandler(
        IScheduleRepository scheduleRepository) : IRequestHandler<SetDefaultScheduleCommand, ErrorOr<Unit>>
    {
        private readonly IScheduleRepository _scheduleRepository = scheduleRepository;

        public async Task<ErrorOr<Unit>> Handle(SetDefaultScheduleCommand command, CancellationToken cancellationToken)
        {
            var employeeId = EmployeeId.Create(command.EmployeeId);

            if (await _scheduleRepository.GetByEmployeeIdAsync(employeeId) is not Schedule schedule)
                return Errors.Schedule.NotFound;

            var workDaySchedules = command.Schedules
                .Select(s => WorkDaySchedule.Create(
                    dayOfWeek: s.DayOfWeek,
                    start: s.Start,
                    end: s.End))
                .ToList();

            schedule.SetDefaultSchedules(workDaySchedules);

            await _scheduleRepository.UpdateAsync(schedule);

            return Unit.Value;
        }
    }
}
