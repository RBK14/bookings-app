using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.ScheduleAggregate.ValueObjects;

namespace Bookings.Domain.ScheduleAggregate.Entities
{
    public sealed class WorkDayOverride : Entity<WorkDayOverrideId>
    {
        public DateOnly Date { get; init; }
        public TimeOnly Start { get; init; }
        public TimeOnly End { get; init; }

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
        private WorkDayOverride()
        {
        }
#pragma warning restore CS8618
    }
}
