using ErrorOr;

namespace Bookings.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation(
                code: "Auth.InvalidCredentials",
                description: "Nieprawidłowy adres email lub hasło."
            );

            public static Error InvalidEmail => Error.Validation(
                code: "Auth.InvalidEmail",
                description: "Adres email jest nieprawidłowy."
            );

            public static Error InvalidPassword => Error.Validation(
                code: "Auth.InvalidPassword",
                description: "Hasło jest nieprawidłowe."
            );

            public static Error InvalidVerificationToken => Error.Validation(
                code: "Auth.InvalidToken",
                description: "Token weryfikacji jest nieprawidłowy."
            );

            public static Error VerificationTokenExpired => Error.Validation(
                code: "Auth.TokenExpired",
                description: "Token weryfikacji jest przeterminowany."
            );

            public static Error VerificationTokenUsed => Error.Validation(
                code: "Auth.TokenUsed",
                description: "Token weryfikacji jest już wykorzystany");

            public static Error EmailNotConfirmed => Error.Unauthorized(
                code: "Auth.EmailNotConfirmed",
                description: "Adres email nie został potwierdzony."
            );
        }
    }
}
