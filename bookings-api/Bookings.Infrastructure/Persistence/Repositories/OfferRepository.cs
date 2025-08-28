using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.OfferAggregate;
using Bookings.Domain.OfferAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly BookingsDbContext _dbContext;

        public OfferRepository(BookingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Offer offer)
        {
            _dbContext.Offers.Add(offer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Offer>> GetOffersAsync()
        {
            return await _dbContext.Offers.ToListAsync();
        }

        public async Task<IEnumerable<Offer>> SearchOffersAsync(
            IEnumerable<IFilterable<Offer>> filters,
            ISortable<Offer> sort)
        {
            var offers = await _dbContext.Offers.ToListAsync();
            var offersQuery = offers.AsQueryable();

            foreach (var filter in filters)
            {
                offersQuery = filter.Apply(offersQuery);
            }

            offersQuery = sort.Apply(offersQuery);

            return offersQuery;
        }

        public async Task<Offer?> GetByIdAsync(OfferId offerId)
        {
            return await _dbContext.Offers.FindAsync(offerId);
        }
    }
}
