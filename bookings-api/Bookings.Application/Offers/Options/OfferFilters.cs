using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate;

namespace Bookings.Application.Offers.Options
{
    public class NameFilter(string? name) : IFilterable<Offer>
    {
        private readonly string? _name = name;

        public IQueryable<Offer> Apply(IQueryable<Offer> query)
        {
            if (string.IsNullOrWhiteSpace(_name))
                return query;

            var lowered = _name.ToLower();

            return query.Where(o => o.Name.ToLower().Contains(lowered));
        }
    }

    public class EmployeeIdFilter(EmployeeId? employeeId) : IFilterable<Offer>
    {
        private readonly EmployeeId? _employeeId = employeeId;

        public IQueryable<Offer> Apply(IQueryable<Offer> query)
        {
            if (_employeeId is not null && !string.IsNullOrWhiteSpace(_employeeId.Value.ToString()))
                query = query.Where(o => o.EmployeeId.Equals(_employeeId));

            return query;
        }
    }

    public class PriceRangeFilter(Price? minPrice, Price? maxPrice) : IFilterable<Offer>
    {
        private readonly Price? _minPrice = minPrice;
        private readonly Price? _maxPrice = maxPrice;

        public IQueryable<Offer> Apply(IQueryable<Offer> query)
        {
            if (_minPrice is not null)
                query = query.Where(o => o.Price.Amount >= _minPrice.Amount);

            if (_maxPrice is not null)
                query = query.Where(o => o.Price.Amount <= _maxPrice.Amount);

            return query;
        }
    }

    public class DurationRangeFilter(Duration? minDuration, Duration? maxDuration) : IFilterable<Offer>
    {
        private readonly Duration? _minDuration = minDuration;
        private readonly Duration? _maxDuration = maxDuration;

        public IQueryable<Offer> Apply(IQueryable<Offer> query)
        {
            if (_minDuration is not null)
                query = query.Where(o => o.Duration.Value >= _minDuration.Value);

            if (_maxDuration is not null)
                query = query.Where(o => o.Duration.Value <= _maxDuration.Value);

            return query;
        }
    }
}
