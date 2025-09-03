using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.ClientAggregate;
using MediatR;

namespace Bookings.Application.Clients.Queries.GetClients
{
    public class GetClientsQueryHandler(IClientRepository clientRepository) : IRequestHandler<GetClientsQuery, IEnumerable<Client>>
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public async Task<IEnumerable<Client>> Handle(GetClientsQuery query, CancellationToken cancellationToken)
        {
            var clients = await _clientRepository.GetAllAsync();

            return clients;
        }
    }
}
