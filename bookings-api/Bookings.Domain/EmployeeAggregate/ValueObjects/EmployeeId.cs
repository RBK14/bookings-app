using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.EmployeeAggregate.ValueObjects
{
    public sealed class EmployeeId : ValueObject
    {
        public Guid Value { get; init; }

        private EmployeeId(Guid value)
        {
            Value = value;
        }

        public static EmployeeId CrateUnique()
        {
            return new EmployeeId(Guid.NewGuid());
        }

        public static EmployeeId Create(Guid value)
        {
            return new EmployeeId(value);
        }

        public static EmployeeId Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Id cannot be empty.");

            if (!Guid.TryParse(value, out var parsed))
                throw new DomainException("Invalid Id format.");

            return new EmployeeId(parsed);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private EmployeeId()
        {
        }
#pragma warning restore CS8618
    }
}
