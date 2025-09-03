using Bookings.Domain.ClientAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Clients.Queries.GetClient
{
    public record GetClientQuery(string Id) : IRequest<ErrorOr<Client>>;
}
