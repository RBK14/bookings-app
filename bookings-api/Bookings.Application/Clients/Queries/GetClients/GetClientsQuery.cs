using Bookings.Domain.ClientAggregate;
using MediatR;

namespace Bookings.Application.Clients.Queries.GetClients
{
    public record GetClientsQuery() : IRequest<IEnumerable<Client>>;
}
