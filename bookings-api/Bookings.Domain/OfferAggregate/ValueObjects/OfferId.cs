using Bookings.Domain.Common.Models;

namespace Bookings.Domain.OfferAggregate.ValueObjects
{
    public sealed class OfferId : ValueObject
    {
        public Guid Value { get; }

        private OfferId(Guid value)
        {
            Value = value;
        }

        public static OfferId CreateUnique()
        {
            return new OfferId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}