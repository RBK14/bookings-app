namespace Bookings.Contracts.Authentication
{
    public record RegisterRequest(
        string Name,
        string Phone,
        string Email,
        string Password);
}
