using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.ScheduleAggregate;
using Bookings.Domain.ScheduleAggregate.Entities;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Schedules.Commands.SetScheduleOverride
{
    public class SetScheduleOverrideCommandHandler(IScheduleRepository scheduleRepository) : IRequestHandler<SetScheduleOverrideCommand, ErrorOr<Unit>>
    {
        private readonly IScheduleRepository _scheduleRepository = scheduleRepository;

        public async Task<ErrorOr<Unit>> Handle(SetScheduleOverrideCommand command, CancellationToken cancellationToken)
        {
            var employeeId = EmployeeId.Create(command.EmployeeId);

            if (await _scheduleRepository.GetByEmployeeIdAsync(employeeId) is not Schedule schedule)
                return Errors.Schedule.NotFound;

            var scheduleOverride = WorkDayOverride.Create(
                date: command.Date,
                start: command.Start,
                end: command.End);

            schedule.SetScheduleOverride(scheduleOverride);

            await _scheduleRepository.UpdateAsync(schedule);

            return Unit.Value;
        }
    }
}
