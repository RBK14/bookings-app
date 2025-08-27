using Bookings.Domain.Common.Models;

namespace Bookings.Domain.AppointmentAggregate.ValueObjects
{
    public sealed class AppointmentId : ValueObject
    {
        public Guid Value { get; init; }

        private AppointmentId(Guid value)
        {
            Value = value;
        }

        public static AppointmentId CreateUnique()
        {
            return new AppointmentId(Guid.NewGuid());
        }

        public static AppointmentId Create(Guid value)
        {
            return new AppointmentId(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private AppointmentId()
        {
        }
#pragma warning restore CS8618
    }
}
