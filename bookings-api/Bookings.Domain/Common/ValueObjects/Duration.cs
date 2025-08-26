using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.Common.ValueObjects
{
    public sealed class Duration : ValueObject
    {
        public TimeSpan Value { get; }

        private Duration(TimeSpan value)
        {
            Value = value;
        }

        public static Duration Create(TimeSpan duration)
        {
            if (duration.TotalMinutes < 15)
                throw new DomainException("Wizyta nie może być krótsza niż 15 minut.");

            if (duration.TotalHours > 8)
                throw new DomainException("Wizyta nie może trwać dłużej niż 4 godziny.");

            return new Duration(duration);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
