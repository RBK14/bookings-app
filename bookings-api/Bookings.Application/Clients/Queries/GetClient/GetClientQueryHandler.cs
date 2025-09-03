using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Clients.Queries.GetClient
{
    public class GetClientQueryHandler(IClientRepository clientRepository) : IRequestHandler<GetClientQuery, ErrorOr<Client>>
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public async Task<ErrorOr<Client>> Handle(GetClientQuery query, CancellationToken cancellationToken)
        {
            var clientId = ClientId.Create(query.Id);

            if (await _clientRepository.GetByIdAsync(clientId) is not Client client)
                return Errors.Client.NotFound;

            return client;
        }
    }
}
