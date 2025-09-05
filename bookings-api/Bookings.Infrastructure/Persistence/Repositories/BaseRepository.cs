using Bookings.Application.Common.Interfaces.Persistence;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class BaseRepository(BookingsDbContext dbContext) : IBaseRepository
    {
        protected readonly BookingsDbContext _dbContext = dbContext;

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
