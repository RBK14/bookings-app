using Bookings.Domain.Common.Models;

namespace Bookings.Domain.EmployeeAggregate.ValueObjects
{
    public sealed class EmployeeId : ValueObject
    {
        public Guid Value { get; }

        private EmployeeId(Guid value)
        {
            Value = value;
        }

        public static EmployeeId CrateUnique()
        {
            return new EmployeeId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
