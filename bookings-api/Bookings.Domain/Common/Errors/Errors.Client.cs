using ErrorOr;

namespace Bookings.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Client
        {
            public static Error NotFound => Error.NotFound(
                code: "Client.NotFound",
                description: "Nie znaleziono żadnego klienta spełniającego wymagania");
        }
    }
}
