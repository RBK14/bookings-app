using Bookings.Domain.ClientAggregate;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IClientRepository
    {
        Task AddAsync(Client client);
        Task<Client?> GetByIdAsync(ClientId clientId);
        Task<Client?> GetByUserIdAsync(UserId userId);
        Task<IEnumerable<Client>> SearchAsync(IEnumerable<IFilterable<Client>>? filters, ISortable<Client>? sort);
        Task UpdateAsync(Client client);
        Task DeleteAsync(Client client);
    }
}
