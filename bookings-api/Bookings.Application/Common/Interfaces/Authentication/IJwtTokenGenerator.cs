using Bookings.Domain.UserAggregate;

namespace Bookings.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
