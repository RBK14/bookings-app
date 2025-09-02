namespace Bookings.Contracts.Appointments
{
    public record UpdateAppointmentRequest(
        DateTime StartTime,
        DateTime EndTime);
}
