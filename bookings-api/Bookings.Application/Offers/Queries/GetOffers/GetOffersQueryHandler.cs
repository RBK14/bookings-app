using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.OfferAggregate;
using MediatR;

namespace Bookings.Application.Offers.Queries.GetOffers
{
    public class GetOffersQueryHandler : IRequestHandler<GetOffersQuery, IEnumerable<Offer>>
    {
        private readonly IOfferRepository _offerRepository;

        public GetOffersQueryHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<IEnumerable<Offer>> Handle(GetOffersQuery query, CancellationToken cancellationToken)
        {
            var offers = await _offerRepository.GetOffersAsync();

            return offers;
        }
    }
}
