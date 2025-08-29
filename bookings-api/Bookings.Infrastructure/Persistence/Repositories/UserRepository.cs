using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class UserRepository(BookingsDbContext dbContext) : IUserRepository
    {
        private readonly BookingsDbContext _dbContext = dbContext;

        public async Task AddAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetByIdAsync(UserId userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<User?> GetByEmailAsync(Email email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email.Value == email.Value);
        }

        public Task<IEnumerable<User>> SearchAsync(IEnumerable<IFilterable<User>> filters, ISortable<User> sort)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
