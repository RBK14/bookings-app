using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.ScheduleAggregate.ValueObjects;

namespace Bookings.Domain.ScheduleAggregate.Entities
{
    public class WorkDaySchedule : Entity<WorkDayScheduleId>
    {
        public DayOfWeek DayOfWeek { get; init; }
        public TimeOnly Start {  get; init; }
        public TimeOnly End { get; init; }

        private WorkDaySchedule(DayOfWeek dayOfWeek, TimeOnly start, TimeOnly end)
        {
            DayOfWeek = dayOfWeek;
            Start = start;
            End = end;
        }

        public static WorkDaySchedule Create(DayOfWeek dayOfWeek, TimeOnly start, TimeOnly end)
        {
            if (end <= start)
                throw new DomainException("End must be after start.");

            return new WorkDaySchedule(dayOfWeek, start, end);
        }

#pragma warning disable CS8618
        protected WorkDaySchedule()
        {
        }
#pragma warning restore CS8618
    }
}
