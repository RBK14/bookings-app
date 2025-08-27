using Bookings.Domain.Common.Models;

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
