using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.CalendarAggregate.ValueObjects
{
    public sealed class CalendarId : ValueObject
    {
        public Guid Value { get; init; }

        private CalendarId(Guid value)
        {
            Value = value;
        }

        public static CalendarId CreateUnique()
        {
            return new CalendarId(Guid.NewGuid());
        }

        public static CalendarId Create(Guid value)
        {
            return new CalendarId(value);
        }

        public static CalendarId Create(string value)
        {

            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Id cannot be empty.");

            if (!Guid.TryParse(value, out var parsed))
                throw new DomainException("Invalid Id format.");

            return new CalendarId(parsed);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private CalendarId()
        {
        }

        
#pragma warning restore CS8618
    }
}
