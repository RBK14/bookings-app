namespace Bookings.Contracts.Schedule
{
    public record WorkHoursResponse(
        DateOnly Date,
        TimeOnly Start,
        TimeOnly End);
}
