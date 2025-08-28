using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.Common.Enums;
using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate.ValueObjects;

namespace Bookings.Domain.OfferAggregate
{
    public class Offer : AggregateRoot<OfferId>
    {
        private readonly List<AppointmentId> _appointmentIds = new();

        public string Name { get; private set; }
        public string Description { get; private set; }
        public EmployeeId EmployeeId { get; init; }
        public Price Price { get; private set; }
        public Duration Duration { get; private set; }
        public IReadOnlyList<AppointmentId> AppointmentIds => _appointmentIds.AsReadOnly();
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; private set; }

        private Offer (
            OfferId offerId,
            string name,
            string description,
            EmployeeId employeeId,
            Price price,
            Duration duration,
            DateTime createdAt,
            DateTime updatedAt) : base(offerId)
        {
            Name = name;
            Description = description;
            EmployeeId = employeeId;
            Price = price;
            Duration = duration;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static Offer Create(
            string name,
            string description,
            EmployeeId employeeId,
            decimal amount,
            Currency currency,
            TimeSpan duration)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Nazwa wizyty nie może być pusta.");

            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("Opis wizyty nie może być pusty.");

            var offer = new Offer(
                OfferId.CreateUnique(),
                name,
                description,
                employeeId,
                Price.Create(amount, currency),
                Duration.Create(duration),
                DateTime.UtcNow,
                DateTime.UtcNow);

            return offer;
        }

        public Offer UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Nazwa wizyty nie może być pusta.");

            Name = name;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public Offer UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("Opis wizyty nie może być pusty.");

            Description = description;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public Offer UpdateDuration(TimeSpan duration)
        {
            Duration = Duration.Create(duration);
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

#pragma warning disable CS8618
        private Offer()
        {
        }
#pragma warning restore CS8618
    }
}
