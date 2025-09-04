using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.ScheduleAggregate.ValueObjects
{
    public sealed class WorkDayScheduleId : ValueObject
    {
        public Guid Value { get; init; }

        private WorkDayScheduleId(Guid value)
        {
            Value = value;
        }

        public static WorkDayScheduleId CreateUnique()
        {
            return new WorkDayScheduleId(Guid.NewGuid());
        }

        public static WorkDayScheduleId Create(Guid value)
        {   
            return new WorkDayScheduleId(value);
        }

        public static WorkDayScheduleId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Id cannot be empty.");

            if (!Guid.TryParse(value, out var parsed))
                throw new DomainException("Invalid Id format.");

            return new WorkDayScheduleId(parsed);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private WorkDayScheduleId()
        {
        }
#pragma warning restore CS8618
    }
}