namespace Bookings.Application.Schedules.Common
{
    public record DayScheduleResultDto(
        DateOnly Date,
        TimeOnly WorkStart,
        TimeOnly WorkEnd,
        IEnumerable<AppointmentSlotResultDto> Appointments);

    public record AppointmentSlotResultDto(
        string AppointmentId,
        TimeOnly Start,
        TimeOnly End,
        string ClientName,
        string OfferName);
}
