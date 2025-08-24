using Bookings.Domain.Entities;

namespace Bookings.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token);
}
