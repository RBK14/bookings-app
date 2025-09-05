using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class UserRepository(BookingsDbContext dbContext) : BaseRepository(dbContext), IUserRepository
    {
        public void Add(User user)
        {
            _dbContext.Users.Add(user);
        }

        public async Task<User?> GetByIdAsync(UserId userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<User?> GetByEmailAsync(Email email)
        {
            return await _dbContext.Users
                .SingleOrDefaultAsync(u => u.Email.Value == email.Value);
        }

        public async Task<IEnumerable<User>> SearchAsync(
            IEnumerable<IFilterable<User>>? filters,
            ISortable<User>? sort)
        {
            var usersQuery = _dbContext.Users.AsQueryable();

            if (filters is not null)
            {
                foreach (var filter in filters)
                {
                    usersQuery = filter.Apply(usersQuery);
                }
            }

            if (sort is not null)
            {
                usersQuery = sort.Apply(usersQuery);
            }

            return await usersQuery.ToListAsync();
        }

        public void Update(User user)
        {
            _dbContext.Users.Update(user);
        }

        public void Delete(User user)
        {
            _dbContext.Users.Remove(user);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
