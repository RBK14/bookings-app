using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate;

namespace Bookings.Infrastructure.Persistence.Offers
{
    public class NameFilter : IFilterable<Offer>
    {
        private readonly string? _name;

        public NameFilter(string? name)
        {
            _name = name;
        }

        public IQueryable<Offer> Apply(IQueryable<Offer> query)
        {
            if (string.IsNullOrWhiteSpace(_name))
                return query;

            return query.Where(o => o.Name.Contains(_name));
        }
    }

    public class EmployeeIdFilter : IFilterable<Offer>
    {
        private readonly EmployeeId? _employeeID;

        public EmployeeIdFilter(EmployeeId? employeeID)
        {
            _employeeID = employeeID;
        }

        public IQueryable<Offer> Apply(IQueryable<Offer> query)
        {
            if (_employeeID is not null && !string.IsNullOrWhiteSpace(_employeeID.Value.ToString()))
                query = query.Where(o => o.EmployeeId.Equals(_employeeID));

            return query;
        }
    }

        public class PriceRangeFilter : IFilterable<Offer>
        {
        private readonly Price? _minPrice;
        private readonly Price? _maxPrice;

        public PriceRangeFilter(Price? minPrice, Price? maxPrice)
        {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
        }

        public IQueryable<Offer> Apply(IQueryable<Offer> query)
        {
            if (_minPrice is not null)
                query = query.Where(o => o.Price.Amount >= _minPrice.Amount);

            if (_maxPrice is not null)
                query = query.Where(o => o.Price.Amount <= _maxPrice.Amount);

            return query;
        }
    }

    public class DurationRangeFilter : IFilterable<Offer>
    {
        private readonly Duration? _minDuration;
        private readonly Duration? _maxDuration;

        public DurationRangeFilter(Duration? minDuration, Duration? maxDuration)
        {
            _minDuration = minDuration;
            _maxDuration = maxDuration;
        }

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
