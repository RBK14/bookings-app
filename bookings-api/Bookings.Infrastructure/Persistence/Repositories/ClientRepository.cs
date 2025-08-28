using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.ClientAggregate.ValueObjects;

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

        public Task<Client> GetById(ClientId clientId)
        {
            throw new NotImplementedException();
        }
    }
}
