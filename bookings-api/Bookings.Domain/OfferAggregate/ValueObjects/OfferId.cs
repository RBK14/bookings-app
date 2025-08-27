using Bookings.Domain.Common.Models;

namespace Bookings.Domain.OfferAggregate.ValueObjects
{
    public sealed class OfferId : ValueObject
    {
        public Guid Value { get; init; }

        private OfferId(Guid value)
        {
            Value = value;
        }

        public static OfferId CreateUnique()
        {
            return new OfferId(Guid.NewGuid());
        }

        public static OfferId Create(Guid value)
        {
            return new OfferId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private OfferId()
        {
        }
#pragma warning restore CS8618
    }
}