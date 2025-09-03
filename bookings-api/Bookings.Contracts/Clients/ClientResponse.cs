namespace Bookings.Contracts.Clients
{
    public record ClientResponse(
        string Id,
        string UserId,
        List<string> AppointmentIds);
}
