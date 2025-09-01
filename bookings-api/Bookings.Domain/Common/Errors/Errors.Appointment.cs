using ErrorOr;

namespace Bookings.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class Appointment
        {
            public static Error NotFound => Error.NotFound(
                code: "Appointment.NotFound",
                description: "Nie znaleziono żadnej wizyty spełniającej wymagania.");
                
        }
    }
}
