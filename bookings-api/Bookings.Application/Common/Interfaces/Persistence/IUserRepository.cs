using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByIdAsync(UserId userId);
        Task<User?> GetByEmailAsync(Email email);
        Task<IEnumerable<User>> SearchAsync(IEnumerable<IFilterable<User>> filters, ISortable<User> sort);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
