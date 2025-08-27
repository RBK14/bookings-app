using Bookings.Domain.OfferAggregate;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IOfferRepository
    {
        Task AddAsync(Offer offer);

        Task<IEnumerable<Offer>> GetOffersAsync();
    }
}
