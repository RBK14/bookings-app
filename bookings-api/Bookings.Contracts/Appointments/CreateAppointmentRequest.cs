namespace Bookings.Contracts.Appointments
{
    public record CreateAppointmentRequest(
        string OfferId,
        DateTime StartTime);
}
