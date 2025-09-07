using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class ClientRepository(BookingsDbContext dbContext) : BaseRepository(dbContext), IClientRepository
    {
        public void Add(Client client)
        {
            _dbContext.Clients.Add(client);
        }

        public async Task<Client?> GetByIdAsync(ClientId clientId)
        {
            return await _dbContext.Clients
                .Include(c => c.AppointmentIds)
                .SingleOrDefaultAsync(c => c.Id == clientId);
        }

        public async Task<Client?> GetByUserIdAsync(UserId userId)
        {
            return await _dbContext.Clients
                .Include(c => c.AppointmentIds)
                .SingleOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _dbContext.Clients
                .Include(c => c.AppointmentIds)
                .ToListAsync();
        }

        public async Task<IEnumerable<Client>> GetByIdsAsync(IEnumerable<ClientId> clientIds)
        {
            return await _dbContext.Clients
                .Include(c => c.AppointmentIds)
                .Where(c => clientIds.Contains(c.Id))
                .ToListAsync();
        }

        public void Update(Client client)
        {
            _dbContext.Clients.Update(client);
        }

        public void Delete(Client client)
        {
            _dbContext.Clients.Remove(client);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
