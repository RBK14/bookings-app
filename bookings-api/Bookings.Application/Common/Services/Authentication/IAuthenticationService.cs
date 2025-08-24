using ErrorOr;

namespace Bookings.Application.Common.Services.Authentication
{
    public interface IAuthenticationService
    {
       ErrorOr<AuthenticationResult> Login(
            string email,
            string password);

       ErrorOr<AuthenticationResult> Register(
            string name,
            string phone,
            string email,
            string password);
    }
}
