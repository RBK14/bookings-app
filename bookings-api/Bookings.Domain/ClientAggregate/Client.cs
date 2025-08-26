using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;

namespace Bookings.Domain.ClientAggregate
{
    public class Client : AggregateRoot<ClientId>
    {
        private readonly List<AppointmentId> _appointmentIds = new();

        public UserId UserId { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Phone Phone { get; private set; }
        public IReadOnlyList<AppointmentId> Appointments => _appointmentIds.AsReadOnly();
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }

        private Client(
            ClientId clientId,
            UserId userId,
            string firstName,
            string lastName,
            Phone phone,
            DateTime createdAt,
            DateTime updatedAt) : base(clientId)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static Client CreateUnique(
            UserId userId,
            string firstName,
            string lastName,
            string phone)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("Imię nie może być puste.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Nazwisko nie może być puste.");

            return new Client(
                ClientId.CrateUnique(),
                userId,
                firstName,
                lastName,
                Phone.Create(phone),
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

        public Client UpdateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("Imię nie może być puste.");

            FirstName = firstName;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public Client UpdateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Nazwisko nie może być puste.");

            LastName = lastName;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public Client UpdatePhone(string phone)
        {
            Phone = Phone.Create(phone);
            UpdatedAt = DateTime.UtcNow;
            return this;
        }
    }
}
