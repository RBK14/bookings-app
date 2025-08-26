using Bookings.Domain.UserAggregate;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetUserByEmail(string email);
    }
}
