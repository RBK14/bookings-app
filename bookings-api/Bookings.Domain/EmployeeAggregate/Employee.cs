using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;

namespace Bookings.Domain.EmployeeAggregate
{
    public class Employee : AggregateRoot<EmployeeId>
    {
        private readonly List<OfferId> _offerIds = new();
        private readonly List<AppointmentId> _appointmentIds = new();

        public UserId UserId { get; init; }
        public IReadOnlyList<OfferId> OfferIds => _offerIds.AsReadOnly();
        public IReadOnlyList<AppointmentId> AppointmentIds => _appointmentIds.AsReadOnly();
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; private set; }

        private Employee(
            EmployeeId employeeId,
            UserId userId,
            DateTime createdAt,
            DateTime updatedAt) : base(employeeId)
        {
            UserId = userId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static Employee Create(UserId userId)
        {
            return new Employee(
                EmployeeId.CrateUnique(),
                userId,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

        public Employee AddOfferId(OfferId offerId)
        {
            if (_offerIds.Contains(offerId))
                throw new DomainException("The offer is already associated with this employee.");

            _offerIds.Add(offerId);
            UpdatedAt = DateTime.UtcNow;

            return this;
        }

        public Employee AddAppointmentId(AppointmentId appointmentId)
        {
            if (_appointmentIds.Contains(appointmentId))
                throw new DomainException("The appointment is already associated with this employee.");

            _appointmentIds.Add(appointmentId);
            UpdatedAt = DateTime.UtcNow;

            return this;
        }

#pragma warning disable CS8618
        private Employee()
        {
        }
#pragma warning restore CS8618
    }
}
