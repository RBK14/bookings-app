namespace Bookings.Application.Schedules.Common
{
    public record FreeDaySlotsResultDto(
        DateOnly Date,
        IEnumerable<FreeSlotResultDto> Slots);

    public record FreeSlotResultDto(
    TimeOnly Start,
    TimeOnly End);
}
