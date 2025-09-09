namespace Bookings.Contracts.Authentication
{
    public record VerificationTokenResponse(
        string Id,
        string Email);
}
