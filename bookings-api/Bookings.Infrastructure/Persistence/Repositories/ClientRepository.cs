using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class ClientRepository(BookingsDbContext dbContext) : IClientRepository
    {
        private readonly BookingsDbContext _dbContext = dbContext;

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
            return await _dbContext.Clients.SingleOrDefaultAsync(c => c.UserId == userId);
        }

        public Task<IEnumerable<Client>> SearchAsync(IEnumerable<IFilterable<Client>>? filters, ISortable<Client>? sort)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Client client)
        {
            _dbContext.Clients.Update(client);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client client)
        {
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }
    }
}
