using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly BookingsDbContext _dbContext;

        public ClientRepository(BookingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Client client)
        {
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Client?> GetByIdAsync(ClientId clientId)
        {
            return await _dbContext.Clients.FindAsync(clientId);
        }

        public async Task<Client?> GetByUserIdAsync(UserId userId)
        {
            return await _dbContext.Clients.SingleOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
