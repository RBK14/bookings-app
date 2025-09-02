using ErrorOr;

namespace Bookings.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error DuplicateEmail => Error.Conflict(
                code: "User.DuplicateEmail",
                description: "Użytownik z takim adresem email już istnieje.");

            public static Error NoPermissions => Error.Forbidden(
                code: "User.NoPermissions",
                description: "Nie można wykonać tej operacji z powodu braku uprawnień.");

            public static Error NotFound => Error.NotFound(
                code: "User.NotFound",
                description: "Nie znaleziono żadnego użytownika spełniającego wymagania");
        }
    }
}


