using Bookings.Domain.OfferAggregate;
using MediatR;

namespace Bookings.Application.Offers.Queries.GetEmployeeOffers
{
    public record GetEmployeeOffersQuery(
        string EmployeeId,
        string? Name,
        decimal? MinPrice,
        decimal? MaxPrice,
        int? Currency,
        string? MinDuration,
        string? MaxDuration,
        string? SortBy) : IRequest<IEnumerable<Offer>>;
}
