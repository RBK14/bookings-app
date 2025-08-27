using Bookings.Domain.OfferAggregate;
using MediatR;

namespace Bookings.Application.Offers.Queries.GetOffers
{
    public record GetOffersQuery() : IRequest<IEnumerable<Offer>>;
}
