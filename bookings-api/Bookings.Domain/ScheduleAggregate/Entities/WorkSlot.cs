using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.ScheduleAggregate.ValueObjects;

namespace Bookings.Domain.ScheduleAggregate.Entities
{
    public sealed class WorkSlot : Entity<WorkSlotId>
    {
        public AppointmentId AppointmentId { get; init; }
        public DateTime Start { get; init; }
        public DateTime End { get; init; }

        private WorkSlot(AppointmentId appointmentId, DateTime start, DateTime end)
        {
            AppointmentId = appointmentId;
            Start = start;
            End = end;
        }

        public static WorkSlot Create(AppointmentId appointmentId, DateTime start, DateTime end)
        {
            if (end <= start)
                throw new DomainException("End must be after start.");

            return new WorkSlot(appointmentId, start, end);
        }

#pragma warning disable CS8618
        private WorkSlot()
        {
        }
#pragma warning restore CS8618
    }
}
