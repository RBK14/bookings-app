namespace Bookings.Contracts.Users
{
    public record UpdateUserRequest(
        string FirstName,
        string LastName,
        string Email,
        string Phone);
}
