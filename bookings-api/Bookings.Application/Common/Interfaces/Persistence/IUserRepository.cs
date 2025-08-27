using Bookings.Domain.UserAggregate;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
