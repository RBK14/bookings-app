using Bookings.Domain.UserAggregate;

namespace Bookings.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token);
}
