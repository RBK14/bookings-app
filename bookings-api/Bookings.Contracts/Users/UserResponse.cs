namespace Bookings.Contracts.Users
{
    public record UserResponse(
        string Id,
        string FirstName,
        string LastName,
        string Email,
        string Phone,
        string Role,
        bool IsEmailConfirmed,
        string ConfirmationCode);
}
