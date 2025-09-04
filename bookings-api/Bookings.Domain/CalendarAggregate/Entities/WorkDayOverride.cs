using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.CalendarAggregate.Entities
{
    public class WorkDayOverride : Entity<int>
    {
        public DateOnly Date { get; private set; }
        public TimeOnly Start { get; private set; }
        public TimeOnly End { get; private set; }

        private WorkDayOverride(DateOnly date, TimeOnly start, TimeOnly end)
        {
            Date = date;
            Start = start;
            End = end;
        }

        public static WorkDayOverride Create(DateOnly date, TimeOnly start, TimeOnly end)
        {
            if (end <= start)
                throw new DomainException("End must be after start.");

            return new WorkDayOverride(date, start, end);
        }

#pragma warning disable CS8618
        protected WorkDayOverride()
        {
        }
#pragma warning restore CS8618
    }
}
