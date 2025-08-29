using Bookings.Domain.OfferAggregate;
using Bookings.Domain.OfferAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IOfferRepository
    {
        Task AddAsync(Offer offer);
        Task<Offer?> GetByIdAsync(OfferId offerId);
        Task<IEnumerable<Offer>> SearchAsync(IEnumerable<IFilterable<Offer>>? filters, ISortable<Offer>? sort);
        Task UpdateAsync(Offer offer);
        Task DeleteAsync(Offer offer);
    }
}
