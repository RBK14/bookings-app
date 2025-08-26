using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;

namespace Bookings.Domain.EmployeeAggregate
{
    public class Employee : AggregateRoot<EmployeeId>
    {
        private readonly List<OfferId> _offerIds = new();
        private readonly List<AppointmentId> _appointmentIds = new();

        public UserId UserId { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Phone Phone { get; private set; }
        public IReadOnlyList<OfferId> Offers => _offerIds.AsReadOnly();
        public IReadOnlyList<AppointmentId> Appointments => _appointmentIds.AsReadOnly();
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }

        private Employee(
            EmployeeId employeeId,
            UserId userId,
            string firstName,
            string lastName,
            Phone phone,
            DateTime createdAt,
            DateTime updatedAt) : base(employeeId)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static Employee CreateUnique(
            UserId userId,
            string firstName,
            string lastName,
            string phone)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("Imię nie może być puste.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Imię nie może być puste.");

            return new Employee(
                EmployeeId.CrateUnique(),
                userId,
                firstName,
                lastName,
                Phone.Create(phone),
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

        public Employee UpdateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("Imię nie może być puste.");

            FirstName = firstName;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public Employee UpdateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Nazwisko nie może być puste.");

            LastName = lastName;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public Employee UpdatePhone(string phone)
        {
            Phone = Phone.Create(phone);
            UpdatedAt = DateTime.UtcNow;
            return this;
        }
    }
}
