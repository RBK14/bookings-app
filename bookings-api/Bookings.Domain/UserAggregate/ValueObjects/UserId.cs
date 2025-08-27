using Bookings.Domain.Common.Models;

namespace Bookings.Domain.UserAggregate.ValueObjects
{
    public sealed class UserId : ValueObject
    {
        public Guid Value { get; init; }

        private UserId(Guid value)
        {
            Value = value;
        }

        public static UserId CreateUnique()
        {
            return new UserId(Guid.NewGuid());
        }

        public static UserId Create(Guid value)
        {
            return new UserId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private UserId()
        {
        }
#pragma warning restore CS8618
    }
}