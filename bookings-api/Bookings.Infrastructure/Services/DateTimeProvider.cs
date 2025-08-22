using Bookings.Application.Common.Interfaces.Authentication;

namespace Bookings.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
