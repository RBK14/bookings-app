using Bookings.Domain.Common.Enums;
using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.Common.ValueObjects
{
    public sealed class Price : ValueObject
    {
        public decimal Amount { get; }
        public Currency Currency { get; }

        private Price(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Price Create(decimal amount, Currency currency)
        {
            if (amount <= 0)
                throw new DomainException("Kwota musi być dodatnia");

            return new Price(amount, currency);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
