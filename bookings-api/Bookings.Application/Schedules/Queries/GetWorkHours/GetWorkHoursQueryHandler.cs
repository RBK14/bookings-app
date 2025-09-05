using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Schedules.Common;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.ScheduleAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Schedules.Queries.GetWorkHours
{
    public class GetWorkHoursQueryHandler(
        IScheduleRepository scheduleRepository) : IRequestHandler<GetWorkHoursQuery, ErrorOr<IEnumerable<WorkHoursResultDto>>>
    {
        private readonly IScheduleRepository _scheduleRepository = scheduleRepository;

        public async Task<ErrorOr<IEnumerable<WorkHoursResultDto>>> Handle(GetWorkHoursQuery query, CancellationToken cancellationToken)
        {
            var employeeId = EmployeeId.Create(query.EmployeeId);

            if (await _scheduleRepository.GetByEmployeeIdAsync(employeeId) is not Schedule schedule)
                return Errors.Schedule.NotFound;

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            DateOnly startDate;
            DateOnly endDate;

            if (query.From.HasValue && query.To.HasValue)
            {
                startDate = query.From.Value;
                endDate = query.To.Value;
            }
            else if (query.Days.HasValue)
            {
                startDate = today;
                endDate = today.AddDays(query.Days.Value);
            }
            else
            {
                startDate = today;
                endDate = today.AddDays(7);
            }

            var result = new List<WorkHoursResultDto>();

            for (var date = startDate; date < endDate; date = date.AddDays(1))
            {
                var overrideDay = schedule.Overrides.FirstOrDefault(o => o.Date == date);
                if (overrideDay is not null)
                {
                    result.Add(new WorkHoursResultDto(date, overrideDay.Start, overrideDay.End));
                    continue;
                }

                var scheduleDay = schedule.DefaultSchedules.FirstOrDefault(s => s.DayOfWeek == date.DayOfWeek);
                if (scheduleDay is not null)
                {
                    result.Add(new WorkHoursResultDto(date, scheduleDay.Start, scheduleDay.End));
                }
            }

            return result;
        }
    }
}
