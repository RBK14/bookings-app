using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.ScheduleAggregate.ValueObjects
{
    public sealed class WorkSlotId : ValueObject
    {
        public Guid Value { get; init; }

        private WorkSlotId(Guid value)
        {
            Value = value;
        }

        public static WorkSlotId CreateUnique()
        {
            return new WorkSlotId(Guid.NewGuid());
        }

        public static WorkSlotId Create(Guid value)
        {   
            return new WorkSlotId(value);
        }

        public static WorkSlotId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Id cannot be empty.");

            if (!Guid.TryParse(value, out var parsed))
                throw new DomainException("Invalid Id format.");

            return new WorkSlotId(parsed);
        }
        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private WorkSlotId()
        {
        }
#pragma warning restore CS8618
    }
}