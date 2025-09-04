using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Offers.Options;
using Bookings.Domain.Common.Enums;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate;
using MediatR;

namespace Bookings.Application.Offers.Queries.SearchOffers
{
    public class GetOffersQueryHandler : IRequestHandler<GetOffersQuery, IEnumerable<Offer>>
    {
        private readonly IOfferRepository _offerRepository;

        public GetOffersQueryHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<IEnumerable<Offer>> Handle(GetOffersQuery query, CancellationToken cancellationToken)
        {
            var employeeId = !string.IsNullOrWhiteSpace(query.EmployeeId) 
                ? EmployeeId.Create(query.EmployeeId)
                : null;

            var currency = CurrencyExtensions.IsCorrectCurrencyCode(query.Currency)
                ? CurrencyExtensions.FromCode(query.Currency)
                : Currency.PLN; // If null => PLN by default
            
            var minPrice = query.MinPrice is decimal
                ? Price.Create(query.MinPrice.Value, currency)
                : null;

            var maxPrice = query.MaxPrice is decimal
                ? Price.Create(query.MaxPrice.Value, currency)
                : null;

            var minDuration = query.MinDuration.HasValue
                ? Duration.Create(query.MinDuration.Value)
                : null;

            var maxDuration = query.MaxDuration.HasValue
                ? Duration.Create(query.MaxDuration.Value)
                : null;

            var filters = new List<IFilterable<Offer>>
            {
                new NameFilter(query.Name),
                new EmployeeIdFilter(employeeId),
                new PriceRangeFilter(minPrice, maxPrice),
                new DurationRangeFilter(minDuration, maxDuration)
            };

            var sort = new OfferSort(query.SortBy);

            return await _offerRepository.SearchAsync(filters, sort);
        }
    }
}
