using ErrorOr;

namespace Bookings.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Offer
        {
            public static Error NotFound => Error.NotFound(
                code: "Offer.NotFound",
                description: "Nie znaleziono żadnej oferty.");
        }
    }
}
