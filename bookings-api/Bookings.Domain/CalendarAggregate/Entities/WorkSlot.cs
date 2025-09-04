using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;

namespace Bookings.Domain.CalendarAggregate.Entities
{
    public class WorkSlot : Entity<int>
    {
        public AppointmentId AppointmentId { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

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
    }
}
