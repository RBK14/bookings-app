namespace Bookings.Contracts.Authentication
{
    public record AuthenticationResponse(
        Guid Id,
        string Name,
        string Phone,
        string Email,
        string Token);
}
