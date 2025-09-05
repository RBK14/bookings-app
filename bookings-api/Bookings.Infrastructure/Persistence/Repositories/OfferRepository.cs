using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.OfferAggregate;
using Bookings.Domain.OfferAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class OfferRepository(BookingsDbContext dbContext) : BaseRepository(dbContext), IOfferRepository
    {
        public void Add(Offer offer)
        {
            _dbContext.Offers.Add(offer);
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
            var offersQuery = _dbContext.Offers
                .Include(o => o.AppointmentIds)
                .AsQueryable();

            if (filters is not null)
            {
                foreach (var filter in filters)
                {
                    offersQuery = filter.Apply(offersQuery);
                }
            }

            if (sort is not null)
            {
                offersQuery = sort.Apply(offersQuery);
            }

            return await offersQuery.ToListAsync();
        }

        public void Update(Offer offer)
        {
            _dbContext.Offers.Update(offer);
        }

        public void Delete(Offer offer)
        {
            _dbContext.Offers.Remove(offer);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
