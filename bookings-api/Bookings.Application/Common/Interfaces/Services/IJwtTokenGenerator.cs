using Bookings.Domain.UserAggregate;

namespace Bookings.Application.Common.Interfaces.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
