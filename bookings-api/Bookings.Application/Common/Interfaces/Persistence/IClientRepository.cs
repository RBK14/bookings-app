using Bookings.Domain.ClientAggregate;
using Bookings.Domain.ClientAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);

        Task<Client> GetById(ClientId clientId);
    }
}
