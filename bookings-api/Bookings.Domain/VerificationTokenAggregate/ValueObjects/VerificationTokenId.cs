using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.VerificationTokenAggregate.ValueObjects
{
    public sealed class VerificationTokenId : ValueObject
    {
        public Guid Value { get; init; }

        private VerificationTokenId(Guid value)
        {
            Value = value;
        }

        public static VerificationTokenId CreateUnique()
        {
            return new VerificationTokenId(Guid.NewGuid());
        }

        public static VerificationTokenId Create(Guid value)
        {
            return new VerificationTokenId(value);
        }

        public static VerificationTokenId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Id cannot be empty.");

            if (!Guid.TryParse(value, out var parsed))
                throw new DomainException("Invalid Id format.");

            return new VerificationTokenId(parsed);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private VerificationTokenId()
        {
        }
#pragma warning restore CS8618
    }
}
