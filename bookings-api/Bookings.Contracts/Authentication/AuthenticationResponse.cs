namespace Bookings.Contracts.Authentication
{
    public record AuthenticationResponse(
        string Id,
        string FirstName,
        string LastName,
        string Phone,
        string Email,
        string Token);
}
