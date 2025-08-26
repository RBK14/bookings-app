using Bookings.Domain.Common.Models;

namespace Bookings.Domain.ClientAggregate.ValueObjects
{
    public sealed class ClientId : ValueObject
    {
        public Guid Value { get; }

        private ClientId(Guid value)
        {
            Value = value;
        }

        public static ClientId CrateUnique()
        {
            return new ClientId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
