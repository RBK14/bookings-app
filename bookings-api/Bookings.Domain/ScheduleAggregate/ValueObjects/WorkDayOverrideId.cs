using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.ScheduleAggregate.ValueObjects
{
    public sealed class WorkDayOverrideId : ValueObject
    {
        public Guid Value { get; init; }

        private WorkDayOverrideId(Guid value)
        {
            Value = value;
        }

        public static WorkDayOverrideId CreateUnique()
        {
            return new WorkDayOverrideId(Guid.NewGuid());
        }

        public static WorkDayOverrideId Create(Guid value)
        {   
            return new WorkDayOverrideId(value);
        }

        public static WorkDayOverrideId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Id cannot be empty.");

            if (!Guid.TryParse(value, out var parsed))
                throw new DomainException("Invalid Id format.");

            return new WorkDayOverrideId(parsed);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private WorkDayOverrideId()
        {
        }
#pragma warning restore CS8618
    }
}