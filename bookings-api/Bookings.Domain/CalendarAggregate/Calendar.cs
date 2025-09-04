using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.CalendarAggregate.Entities;
using Bookings.Domain.CalendarAggregate.ValueObjects;
using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bookings.Domain.CalendarAggregate
{
    public class Calendar : AggregateRoot<CalendarId>
    {
        private readonly List<WorkDaySchedule> _defaultSchedules = new();
        private readonly List<WorkDayOverride> _overrides = new();
        private readonly List<WorkSlot> _slots = new();

        public EmployeeId EmployeeId { get; init; }
        public IReadOnlyList<WorkDaySchedule> DefaultSchedules => _defaultSchedules.AsReadOnly();
        public IReadOnlyList<WorkDayOverride> Overrides => _overrides.AsReadOnly();
        public IReadOnlyList<WorkSlot> Slots => _slots.AsReadOnly();
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; private set; }

        private Calendar(
            CalendarId calendarId,
            EmployeeId employeeId,
            IEnumerable<WorkDaySchedule> defaultSchedules,
            DateTime createdAt,
            DateTime updatedAt) : base(calendarId)
        {
            EmployeeId = employeeId;
            _defaultSchedules = defaultSchedules.ToList();
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static Calendar Create(
            EmployeeId employeeId,
            IEnumerable<WorkDaySchedule> defaultSchedules)
        {
            var calendar = new Calendar(
                CalendarId.CreateUnique(),
                employeeId,
                defaultSchedules,
                DateTime.UtcNow,
                DateTime.UtcNow);

            return calendar;
        }

        public void SetDefaultSchedules(IEnumerable<WorkDaySchedule> schedules)
        {
            _defaultSchedules.Clear();
            _defaultSchedules.AddRange(schedules);
            UpdatedAt = DateTime.UtcNow;
        }

        public void OverrideDay(DateOnly date, TimeOnly start, TimeOnly end)
        {
            _overrides.RemoveAll(o => o.Date == date);
            _overrides.Add(WorkDayOverride.Create(date, start, end));
            UpdatedAt = DateTime.UtcNow;
        }

        public bool IsSlotAvailable(DateTime start, DateTime end)
        {
            if (end <= start)
                return false;

            var date = DateOnly.FromDateTime(start);
            var schedule = GetScheduleForDate(date);

            if (schedule is null)
                return false;

            var startTime = TimeOnly.FromDateTime(start);
            var endTime = TimeOnly.FromDateTime(end);

            if (startTime < schedule.Start || endTime > schedule.End)
                return false;

            bool overlaps = _slots.Any(s =>
                s.Start < end && s.End > start);

            return !overlaps;
        }

        public void BoolSlot(AppointmentId appointmentId, DateTime start, DateTime end)
        {
            if (!IsSlotAvailable(start, end))
                throw new DomainException("Selected time is not available.");

            _slots.Add(WorkSlot.Create(appointmentId, start, end));
            UpdatedAt = DateTime.UtcNow;
        }

        private WorkDaySchedule? GetScheduleForDate(DateOnly date)
        {
            var overrideDay = _overrides.FirstOrDefault(o => o.Date == date);
            if (overrideDay is not null)
            {
                return WorkDaySchedule.Create(
                    date.DayOfWeek,
                    overrideDay.Start,
                    overrideDay.End);
            }

            return _defaultSchedules.FirstOrDefault(s => s.DayOfWeek == date.DayOfWeek);
        }

    }
}
