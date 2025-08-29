using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.OfferAggregate.ValueObjects;

namespace Bookings.Domain.ClientAggregate.ValueObjects
{
    public sealed class ClientId : ValueObject
    {
        public Guid Value { get; init; }

        private ClientId(Guid value)
        {
            Value = value;
        }

        public static ClientId CrateUnique()
        {
            return new ClientId(Guid.NewGuid());
        }

        public static ClientId Create(Guid value)
        {
            return new ClientId(value);
        }

        public static ClientId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Id cannot be empty.");

            if (!Guid.TryParse(value, out var parsed))
                throw new DomainException("Invalid Id format.");

            return new ClientId(parsed);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private ClientId()
        {
        }
#pragma warning restore CS8618
    }
}
