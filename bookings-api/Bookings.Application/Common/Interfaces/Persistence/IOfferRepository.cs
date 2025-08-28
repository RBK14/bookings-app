using Bookings.Domain.OfferAggregate;
using Bookings.Domain.OfferAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IOfferRepository
    {
        Task AddAsync(Offer offer);

        Task<IEnumerable<Offer>> GetOffersAsync(IEnumerable<IFilterable<Offer>>? filters, ISortable<Offer>? sort);

        Task<Offer?> GetByIdAsync(OfferId offerId);

    }
}
