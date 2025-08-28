using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.OfferAggregate;

namespace Bookings.Application.Offers.Filters
{
    public class OfferSort : ISortable<Offer>
    {
        private readonly string? _sortBy;

        public OfferSort(string? sortBy)
        {
            _sortBy = sortBy;
        }

        public IQueryable<Offer> Apply(IQueryable<Offer> query)
        {
            return _sortBy switch
            {
                "NameAsc" => query.OrderBy(o => o.Name),
                "NameDesc" => query.OrderByDescending(o => o.Name),
                "PriceAsc" => query.OrderBy(o => o.Price.Amount),
                "PriceDesc" => query.OrderByDescending(o => o.Price.Amount),
                "DurationAsc" => query.OrderBy(o => o.Duration.Value),
                "DurationDesc" => query.OrderByDescending(o => o.Duration.Value),
                _ => query
            };
        }
    }

}
