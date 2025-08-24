using Bookings.Domain.Entities;

namespace Bookings.Application.Common.Interfaces.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
