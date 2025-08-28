using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookingsDbContext _dbContext;

        public UserRepository(BookingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(u => u.Email.Value == email);
        }
    }
}
