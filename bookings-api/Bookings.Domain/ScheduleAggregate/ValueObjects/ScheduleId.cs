using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.ScheduleAggregate.ValueObjects
{
    public sealed class ScheduleId : ValueObject
    {
        public Guid Value { get; init; }

        private ScheduleId(Guid value)
        {
            Value = value;
        }

        public static ScheduleId CreateUnique()
        {
            return new ScheduleId(Guid.NewGuid());
        }

        public static ScheduleId Create(Guid value)
        {   
            return new ScheduleId(value);
        }

        public static ScheduleId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Id cannot be empty.");

            if (!Guid.TryParse(value, out var parsed))
                throw new DomainException("Invalid Id format.");

            return new ScheduleId(parsed);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private ScheduleId()
        {
        }
#pragma warning restore CS8618
    }
}