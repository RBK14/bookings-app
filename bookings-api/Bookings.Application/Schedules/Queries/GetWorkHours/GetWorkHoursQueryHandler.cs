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

            DateOnly fromDate;
            DateOnly toDate;

            if (query.From.HasValue && query.To.HasValue)
            {
                fromDate = query.From.Value;
                toDate = query.To.Value;
            }
            else if (query.Days.HasValue)
            {
                fromDate = today;
                toDate = today.AddDays(query.Days.Value);
            }
            else
            {
                fromDate = today;
                toDate = today.AddDays(7);
            }

            var results = new List<WorkHoursResultDto>();

            for (var date = fromDate; date < toDate; date = date.AddDays(1))
            {
                var overrideDay = schedule.Overrides.FirstOrDefault(o => o.Date == date);
                if (overrideDay is not null)
                {
                    results.Add(new WorkHoursResultDto(date, overrideDay.Start, overrideDay.End));
                    continue;
                }

                var scheduleDay = schedule.DefaultSchedules.FirstOrDefault(s => s.DayOfWeek == date.DayOfWeek);
                if (scheduleDay is not null)
                {
                    results.Add(new WorkHoursResultDto(date, scheduleDay.Start, scheduleDay.End));
                }
            }

            return results;
        }
    }
}
