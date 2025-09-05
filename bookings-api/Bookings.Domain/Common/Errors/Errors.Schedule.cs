using ErrorOr;

namespace Bookings.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Schedule
        {
            public static Error NotFound => Error.NotFound(
                code: "Schedule.NotFound",
                description: "Nie znaleziono żadnego terminarza spełniającego wymagania");
        }
    }
}
