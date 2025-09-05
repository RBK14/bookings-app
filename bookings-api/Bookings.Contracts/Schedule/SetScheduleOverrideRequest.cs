namespace Bookings.Contracts.Schedule
{
    public record SetScheduleOverrideRequest(
        DateOnly Date,
        TimeOnly Start,
        TimeOnly End);
}
