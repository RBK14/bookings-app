using Bookings.Domain.ClientAggregate;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IClientRepository : IBaseRepository
    {
        void Add(Client client);
        Task<Client?> GetByIdAsync(ClientId clientId);
        Task<Client?> GetByUserIdAsync(UserId userId);
        Task<IEnumerable<Client>> GetAllAsync();
        void Update(Client client);
        void Delete(Client client);
    }
}
