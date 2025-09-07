using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository : IBaseRepository
    {
        void Add(User user);
        Task<User?> GetByIdAsync(UserId userId);
        Task<User?> GetByEmailAsync(Email email);
        Task<IEnumerable<User>> SearchAsync(IEnumerable<IFilterable<User>> filters, ISortable<User> sort);
        Task<IEnumerable<User>> GetByIdsAsync(IEnumerable<UserId> userIds);
        void Update(User user);
        void Delete(User user);
    }
}
