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

        public async Task UpdateAsync(Client client)
        {
            _dbContext.Clients.Update(client);
            //await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client client)
        {
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }
    }
}
