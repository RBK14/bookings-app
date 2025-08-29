using ErrorOr;
using MediatR;

namespace Bookings.Application.Offers.Commands.DeleteOffer
{
    public record DeleteOfferCommand(
        string OfferId,
        string EmployeeId) : IRequest<ErrorOr<Unit>>;
}
