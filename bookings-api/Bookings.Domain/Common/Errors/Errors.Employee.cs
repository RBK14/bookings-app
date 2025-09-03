using ErrorOr;

namespace Bookings.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Employee
        {
            public static Error NotFound => Error.NotFound(
                code: "Employee.NotFound",
                description: "Nie znaleziono żadnego pracownika spełniającego wymagania");
        }
    }
}
