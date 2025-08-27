using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.Common.ValueObjects;

namespace Bookings.Domain.AppointmentAggregate.ValueObjects
{
    public class AppointmentTime : ValueObject
    {
        public DateTime Start { get; init; }
        public DateTime End { get; init; }

        private AppointmentTime(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public static AppointmentTime Create(DateTime start, Duration duration)
        {
            if (duration is null)
                throw new DomainException("Duration cannot be empty.");

            var end = start.Add(duration.Value);

            if (end <= start)
                throw new DomainException("Start hour must be before end hour.");

            return new AppointmentTime(start, end);
        }

        public static AppointmentTime CreateFixed(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new DomainException("Start hour must be before end hour.");

            return new(start, end);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }

#pragma warning disable CS8618
        private AppointmentTime()
        {
        }
#pragma warning restore CS8618
    }
}
