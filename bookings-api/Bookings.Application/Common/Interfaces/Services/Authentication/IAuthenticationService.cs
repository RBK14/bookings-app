namespace Bookings.Application.Common.Interfaces.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Login(
            string email,
            string password);

        AuthenticationResult Register(
            string name,
            string phone,
            string email,
            string password);
    }
}
