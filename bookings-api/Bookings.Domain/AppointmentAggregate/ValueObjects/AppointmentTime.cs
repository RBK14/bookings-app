using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.Common.ValueObjects;

namespace Bookings.Domain.AppointmentAggregate.ValueObjects
{
    public class AppointmentTime : ValueObject
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        private AppointmentTime(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public static AppointmentTime Create(DateTime start, Duration duration)
        {
            if (duration is null)
                throw new DomainException("Czas trwania wizyty nie może być pusty.");

            var end = start.Add(duration.Value);

            if (end <= start)
                throw new DomainException("Godzina zakończenia musi być późniejsza niż rozpoczęcia.");

            return new AppointmentTime(start, end);
        }

        public static AppointmentTime CreateFixed(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new DomainException("Godzina zakończenia musi być późniejsza niż rozpoczęcia.");

            return new(start, end);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }
}
