using Bookings.Domain.AppointmentAggregate.Enums;
using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate.ValueObjects;

namespace Bookings.Domain.AppointmentAggregate
{
    public class Appointment : AggregateRoot<AppointmentId>
    {
        public OfferId OfferId { get; init; }

        // Snapshot of the offer
        public string OfferName { get; init; }
        public Price OfferPrice { get; init; }
        public Duration OfferDuration { get; init; }

        public EmployeeId EmployeeId { get; init; }
        public ClientId ClientId { get; init; }
        public AppointmentTime Time { get; private set; }
        public AppointmentStatus Status { get; private set; }
        public CancellationBy CancelledBy { get; private set; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; private set; }

        private Appointment (
            AppointmentId appointmentId,
            OfferId offerId,
            string offerName,
            Price offerPrice,
            Duration offerDuration,
            EmployeeId employeeId,
            ClientId clientId,
            AppointmentTime appointmentTime,
            AppointmentStatus appointmentStatus,
            CancellationBy cancelledBy,
            DateTime createdAt,
            DateTime updatedAt) : base(appointmentId)
        {
            OfferId = offerId;
            OfferName = offerName;
            OfferPrice = offerPrice;
            OfferDuration = offerDuration;
            EmployeeId = employeeId;
            ClientId = clientId;
            Time = appointmentTime;
            Status = appointmentStatus;
            CancelledBy = cancelledBy;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static Appointment CreateUnique(
            OfferId offerId,
            string offerName,
            Price offerPrice,
            Duration offerDuration,
            EmployeeId employeeId,
            ClientId clientId,
            DateTime startTime,
            AppointmentStatus appointmentStatus,
            CancellationBy cancelledBy)
        {
            return new Appointment(
                AppointmentId.CreateUnique(),
                offerId,
                offerName,
                offerPrice,
                offerDuration,
                employeeId,
                clientId,
                AppointmentTime.Create(startTime, offerDuration),
                AppointmentStatus.Pending,
                CancellationBy.None,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

        public Appointment Confirm()
        {
            if (Status != AppointmentStatus.Pending)
                throw new DomainException("Można potwierdzić tylko wizytę w stanie Pending.");

            Status = AppointmentStatus.Confirmed;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public Appointment Complete()
        {
            if (Status != AppointmentStatus.Confirmed)
                throw new DomainException("Można zakończyć tylko potwierdzoną lub trwającą wizytę.");

            Status = AppointmentStatus.Confirmed;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public Appointment Cancel(CancellationBy by)
        {
            if (Status == AppointmentStatus.Completed)
                throw new DomainException("Nie można anulować zakończonej wizyty.");

            if (Status == AppointmentStatus.Cancelled)
                throw new DomainException("Wizyta jest już anulowana.");

            Status = AppointmentStatus.Cancelled;
            CancelledBy = by;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public Appointment MarkNoShow()
        {
            if (Status != AppointmentStatus.Confirmed)
                throw new DomainException("Tylko potwierdzona wizyta może zostać oznaczona jako NoShow.");

            Status = AppointmentStatus.NoShow;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public Appointment UpdateTime(DateTime start, DateTime end)
        {
            Time = AppointmentTime.CreateFixed(start, end);
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

#pragma warning disable CS8618
        private Appointment()
        {
        }
#pragma warning restore CS8618
    }
}
