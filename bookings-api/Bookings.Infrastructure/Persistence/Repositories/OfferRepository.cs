using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.OfferAggregate;
using Bookings.Domain.OfferAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class OfferRepository(BookingsDbContext dbContext) : IOfferRepository
    {
        private readonly BookingsDbContext _dbContext = dbContext;

        public async Task AddAsync(Offer offer)
        {
            _dbContext.Offers.Add(offer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Offer?> GetByIdAsync(OfferId offerId)
        {
            return await _dbContext.Offers
                .Include(o => o.AppointmentIds)
                .SingleOrDefaultAsync(o => o.Id == offerId);
        }

        public async Task<IEnumerable<Offer>> SearchAsync(
            IEnumerable<IFilterable<Offer>>? filters,
            ISortable<Offer>? sort)
        {
            var offers = await _dbContext.Offers
                .Include(o => o.AppointmentIds)
                .ToListAsync();

            var offersQuery = offers.AsQueryable();

            if (filters is not null)
            {
                foreach (var filter in filters)
                {
                    offersQuery = filter.Apply(offersQuery);
                }
            }
            
            if (sort is not null)
                offersQuery = sort.Apply(offersQuery);

            return offersQuery;
        }

        public async Task UpdateAsync(Offer offer)
        {
            _dbContext.Offers.Update(offer);
            //await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Offer offer)
        {
            _dbContext.Offers.Remove(offer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
