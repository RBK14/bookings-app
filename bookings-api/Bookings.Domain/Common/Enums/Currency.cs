using Bookings.Domain.Common.Exceptions;

namespace Bookings.Domain.Common.Enums
{
    public enum Currency
    {
        PLN = 985
    }

    public static class CurrencyExtensions
    {
        public static Currency FromCode(int? code)
        {
            if (IsCorrectCurrencyCode(code))
            {
                return (Currency)code!;
            }
            throw new DomainException($"Unknown currency code: {code}");
        }

        public static bool IsCorrectCurrencyCode(int? code)
        {
            if (code is null || !code.HasValue)
                return false;

            return Enum.IsDefined(typeof(Currency), code.Value);
        }
    }
}
