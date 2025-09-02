using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.OfferAggregate.ValueObjects;

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

        public static UserId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Id cannot be empty.");

            if (!Guid.TryParse(value, out var parsed))
                throw new DomainException("Invalid Id format.");

            return new UserId(parsed);
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