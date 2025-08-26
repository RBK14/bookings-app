using Bookings.Domain.Common.Models;

namespace Bookings.Domain.AppointmentAggregate.ValueObjects
{
    public sealed class AppointmentId : ValueObject
    {
        public Guid Value { get; }

        private AppointmentId(Guid value)
        {
            Value = value;
        }

        public static AppointmentId CreateUnique()
        {
            return new AppointmentId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
