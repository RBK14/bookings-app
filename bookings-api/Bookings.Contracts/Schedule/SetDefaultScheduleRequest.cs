namespace Bookings.Contracts.Schedule
{
    public record SetDefaultScheduleRequest(
        List<WorkDayScheduleDto> Schedules
    );

    public record WorkDayScheduleDto(
        DayOfWeek DayOfWeek,
        TimeOnly Start,
        TimeOnly End
    );
}
