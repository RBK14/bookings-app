using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.EmployeeAggregate.ValueObjects;

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

        public static OfferId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Id cannot be empty.");

            if (!Guid.TryParse(value, out var parsed))
                throw new DomainException("Invalid Id format.");

            return new OfferId(parsed);
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