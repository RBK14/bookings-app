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
            await _dbContext.Offers.AddAsync(offer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Offer>> GetOffersAsync()
        {
            return await _dbContext.Offers.ToListAsync();
        }

        public async Task<Offer?> GetByIdAsync(OfferId offerId)
        {
            return await _dbContext.Offers.FindAsync(offerId);
        }
    }
}
