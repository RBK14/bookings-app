using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.Common.ValueObjects
{
    public sealed class Duration : ValueObject
    {
        public TimeSpan Value { get; init; }

        private Duration(TimeSpan value)
        {
            Value = value;
        }

        public static Duration Create(TimeSpan duration)
        {
            if (duration.TotalMinutes < 15)
                throw new DomainException("Wizyta nie może być krótsza niż 15 minut.");

            if (duration.TotalHours > 8)
                throw new DomainException("Wizyta nie może trwać dłużej niż 8 godzin.");

            return new Duration(duration);
        }

        public static TimeSpan Parse(string duration)
        {
            if (string.IsNullOrWhiteSpace(duration))
                throw new DomainException("Czas trwania nie może być pusty.");

            if (!TimeSpan.TryParse(duration, out var parsed))
                throw new DomainException("Nieprawidłowy format czasu.");
            return parsed;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private Duration()
        {
        }
#pragma warning restore CS8618
    }
}
