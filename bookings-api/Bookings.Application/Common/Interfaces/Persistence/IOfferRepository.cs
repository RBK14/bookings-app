using Bookings.Domain.OfferAggregate;
using Bookings.Domain.OfferAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IOfferRepository : IBaseRepository
    {
        void Add(Offer offer);
        Task<Offer?> GetByIdAsync(OfferId offerId);
        Task<IEnumerable<Offer>> SearchAsync(IEnumerable<IFilterable<Offer>>? filters, ISortable<Offer>? sort);
        void Update(Offer offer);
        void Delete(Offer offer);
    }
}
