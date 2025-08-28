using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Offers.Filters;
using Bookings.Domain.Common.Enums;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate;
using Bookings.Infrastructure.Persistence.Offers;
using MediatR;

namespace Bookings.Application.Offers.Queries.GetEmployeeOffers
{
    public class GetEmployeeOffersQueryHandler : IRequestHandler<GetEmployeeOffersQuery, IEnumerable<Offer>>
    {
        private readonly IOfferRepository _offerRepository;

        public GetEmployeeOffersQueryHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<IEnumerable<Offer>> Handle(GetEmployeeOffersQuery query, CancellationToken cancellationToken)
        {
            var employeeId = EmployeeId.Create(query.EmployeeId);

            var currency = CurrencyExtensions.IsCorrectCurrencyCode(query.Currency)
                ? CurrencyExtensions.FromCode(query.Currency)
                : Currency.PLN; // If null => PLN by default

            var minPrice = query.MinPrice is decimal
                ? Price.Create(query.MinPrice.Value, currency)
                : null;

            var maxPrice = query.MaxPrice is decimal
                ? Price.Create(query.MaxPrice.Value, currency)
                : null;

            var minDuration = !string.IsNullOrWhiteSpace(query.MinDuration)
                ? Duration.Create(query.MinDuration)
                : null;

            var maxDuration = !string.IsNullOrWhiteSpace(query.MaxDuration)
                ? Duration.Create(query.MaxDuration)
                : null;

            var filter = new List<IFilterable<Offer>>
            {
                new NameFilter(query.Name),
                new EmployeeIdFilter(employeeId),
                new PriceRangeFilter(minPrice, maxPrice),
                new DurationRangeFilter(minDuration, maxDuration)
            };

            var sort = new OfferSort(query.SortBy);

            return await _offerRepository.GetOffersAsync(filter, sort);
        }
    }
}
