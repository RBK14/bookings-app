namespace Bookings.Contracts.Authentication
{
    public record UpdatePasswordRequest(
        string CurrentPassword,
        string NewPassword,
        string ConfirmNewPassword);
}
