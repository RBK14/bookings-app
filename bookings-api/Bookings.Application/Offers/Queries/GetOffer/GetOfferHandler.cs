using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.OfferAggregate;
using Bookings.Domain.OfferAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Offers.Queries.GetOffer
{
    public class GetOfferHandler : IRequestHandler<GetOfferQuery, ErrorOr<Offer>>
    {
        private readonly IOfferRepository _offerRepository;

        public GetOfferHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<ErrorOr<Offer>> Handle(GetOfferQuery query, CancellationToken cancellationToken)
        {
            var offerId = OfferId.Create(query.OfferId);

            var offer = await _offerRepository.GetByIdAsync(offerId);

            if (offer is null)
                return Errors.Offer.NotFound;

            return offer;
        }
    }
}
