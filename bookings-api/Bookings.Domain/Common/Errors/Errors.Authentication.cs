using ErrorOr;

namespace Bookings.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation(
                code: "Auth.InvalidCredentials",
                description: "Nieprawidłowy email lub hasło.");

            public static Error EmailNotConfirmed => Error.Unauthorized(
                code: "Auth.EmailNotConfirmed",
                description: "Adres email nie został potwierdzony.");
        }
    }
}
