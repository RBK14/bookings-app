using Bookings.Application.Common.Interfaces.Services;

namespace Bookings.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
