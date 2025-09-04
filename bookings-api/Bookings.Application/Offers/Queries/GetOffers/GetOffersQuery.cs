using Bookings.Domain.OfferAggregate;
using MediatR;

namespace Bookings.Application.Offers.Queries.SearchOffers
{
    public record GetOffersQuery(
        string? Name,
        string? EmployeeId,
        decimal? MinPrice,
        decimal? MaxPrice,
        int? Currency,
        TimeSpan? MinDuration,
        TimeSpan? MaxDuration,
        string? SortBy) : IRequest<IEnumerable<Offer>>;

}
