using Bookings.Domain.OfferAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Offers.Commands.CreateOffer
{
    public record CreateOfferCommand(
        string EmployeeId,
        string Name,
        string Description,
        decimal Amount,
        int Currency,
        string Duration): IRequest<ErrorOr<Offer>>;

}
