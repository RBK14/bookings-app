namespace Bookings.Contracts.Schedule
{
    public record DayFreeSlotsResponse(
        DateOnly Date,
        List<FreeSlotDto> Slots);

    public record FreeSlotDto(
        TimeOnly Start,
        TimeOnly End);
}
