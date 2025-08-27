using Bookings.Domain.OfferAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Offers.Queries.GetOfferById
{
    public record GetOfferByIdQuery(string OfferId) : IRequest<ErrorOr<Offer>>;
}
