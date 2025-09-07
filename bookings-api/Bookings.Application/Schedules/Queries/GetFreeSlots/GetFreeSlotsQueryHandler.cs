using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Schedules.Common;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.OfferAggregate;
using Bookings.Domain.OfferAggregate.ValueObjects;
using Bookings.Domain.ScheduleAggregate;
using Bookings.Domain.ScheduleAggregate.Entities;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Schedules.Queries.GetFreeSlots
{
    public class GetFreeSlotsQueryHandler(
        IScheduleRepository scheduleRepository,
        IOfferRepository offerRepository) : IRequestHandler<GetFreeSlotsQuery, ErrorOr<IEnumerable<FreeDaySlotsResultDto>>>
    {
        private readonly IScheduleRepository _scheduleRepository = scheduleRepository;
        private readonly IOfferRepository _offerRepository = offerRepository;

        public async Task<ErrorOr<IEnumerable<FreeDaySlotsResultDto>>> Handle(GetFreeSlotsQuery query, CancellationToken cancellationToken)
        {
            var fromDate = query.From ?? DateOnly.FromDateTime(DateTime.UtcNow);
            var toDate = fromDate.AddDays(query.Days);

            var offerId = OfferId.Create(query.OfferId);
            if (await _offerRepository.GetByIdAsync(offerId) is not Offer offer)
                return Errors.Offer.NotFound;

            var employeeId = offer.EmployeeId;
            if (await _scheduleRepository.GetByEmployeeIdAsync(employeeId) is not Schedule schedule)
                return Errors.Schedule.NotFound;

            var results = new List<FreeDaySlotsResultDto>();

            foreach (var date in EachDate(fromDate, toDate))
            {
                var overrideDay = schedule.Overrides.FirstOrDefault(o => o.Date == date);
                if (overrideDay is not null)
                {
                    var overrideOccupiedSlots = schedule.Slots
                        .Where(s => DateOnly.FromDateTime(s.Start) == date)
                        .OrderBy(s => s.Start)
                        .ToList();

                    var overrideFreeSlots = CalculateFreeSlots(
                        date,
                        overrideDay.Start,
                        overrideDay.End,
                        overrideOccupiedSlots,
                        offer.Duration.Value);

                    if (overrideFreeSlots.Any())
                        results.Add(new FreeDaySlotsResultDto(date, overrideFreeSlots));

                    continue;
                }

                var scheduleDay = schedule.DefaultSchedules.FirstOrDefault(s => s.DayOfWeek == date.DayOfWeek);
                if (scheduleDay is null)
                    continue;

                var occupiedSlots = schedule.Slots
                    .Where(s => DateOnly.FromDateTime(s.Start) == date)
                    .OrderBy(s => s.Start)
                    .ToList();

                var freeSlots = CalculateFreeSlots(
                    date,
                    scheduleDay.Start,
                    scheduleDay.End,
                    occupiedSlots,
                    offer.Duration.Value);

                if (freeSlots.Any())
                    results.Add(new FreeDaySlotsResultDto(date, freeSlots));
            }

            return results;
        }

        private static IEnumerable<DateOnly> EachDate(DateOnly from, DateOnly to)
        {
            for (var date = from; date <= to; date = date.AddDays(1))
                yield return date;
        }

        private IEnumerable<FreeSlotResultDto> CalculateFreeSlots(
            DateOnly date,
            TimeOnly workStart,
            TimeOnly workEnd,
            IEnumerable<WorkSlot> occupiedSlots,
            TimeSpan duration)
        {
            var slots = new List<FreeSlotResultDto>();

            var currentStart = date.ToDateTime(workStart);

            foreach (var slot in occupiedSlots)
            {
                if (slot.Start > currentStart)
                {
                    var freeEnd = slot.Start;
                    AddSlots(slots, currentStart, freeEnd, duration);
                }

                currentStart = slot.End > currentStart
                    ? slot.End
                    : currentStart;
            }

            var dayEnd = date.ToDateTime(workEnd);
            if (dayEnd > currentStart)
                AddSlots(slots, currentStart, dayEnd, duration);

            return slots;
        }

        private void AddSlots(List<FreeSlotResultDto> slots, DateTime start, DateTime end, TimeSpan duration)
        {
            while (start.Add(duration) <= end)
            {
                slots.Add(new FreeSlotResultDto(
                    TimeOnly.FromDateTime(start),
                    TimeOnly.FromDateTime(start.Add(duration))
                ));

                // Przesunięcie o interwał (czas trwania wizyty)
                start = start.Add(duration);
            }
        }
    }
}
