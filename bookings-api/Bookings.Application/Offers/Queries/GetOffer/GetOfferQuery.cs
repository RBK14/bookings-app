using Bookings.Domain.OfferAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Offers.Queries.GetOffer
{
    public record GetOfferQuery(string OfferId) : IRequest<ErrorOr<Offer>>;
}
