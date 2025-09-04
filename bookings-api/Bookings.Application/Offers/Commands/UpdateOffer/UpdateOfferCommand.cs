using Bookings.Domain.OfferAggregate;
using ErrorOr;
using MediatR;


namespace Bookings.Application.Offers.Commands.UpdateOffer
{
    public record UpdateOfferCommand (
        string OfferId,
        string EmployeeId,
        string Name,
        string Description,
        decimal Amount,
        int Currency,
        TimeSpan Duration) : IRequest<ErrorOr<Offer>>;
}
