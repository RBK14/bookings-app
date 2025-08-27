using Bookings.Domain.Common.Exceptions;

namespace Bookings.Domain.Common.Enums
{
    public enum Currency
    {
        PLN = 985,
        EUR = 978,
        USD = 840
    }

    public static class CurrencyExtensions
    {
        public static Currency FromCode(int code)
        {
            if (Enum.IsDefined(typeof(Currency), code))
            {
                return (Currency)code;
            }
            throw new DomainException($"Unknown currency code: {code}");
        }
    }
}
