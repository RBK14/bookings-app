using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.CalendarAggregate.Entities
{
    public class WorkDaySchedule : Entity<int>
    {
        public DayOfWeek DayOfWeek { get; private set; }
        public TimeOnly Start { get; private set; }
        public TimeOnly End { get; private set; }

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
