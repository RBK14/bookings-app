using Bookings.Domain.OfferAggregate;
using MediatR;

namespace Bookings.Application.Offers.Queries.SearchOffers
{
    public record SearchOffersQuery(
        string? Name,
        string? EmployeeId,
        decimal? MinPrice,
        decimal? MaxPrice,
        int? Currency,
        string? MinDuration,
        string? MaxDuration,
        string? SortBy) : IRequest<IEnumerable<Offer>>;

}
