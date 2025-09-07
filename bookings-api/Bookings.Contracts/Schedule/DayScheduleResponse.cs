namespace Bookings.Contracts.Schedule
{
    public record DayScheduleResponse(
        DateOnly Date,
        TimeOnly WorkStart,
        TimeOnly WorkEnd,
        List<AppointmentSlotDto> Appointments);

    public record AppointmentSlotDto(
        string AppointmentId,
        TimeOnly Start,
        TimeOnly End,
        string OfferName,
        string ClientName);
}
