using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate;
using Bookings.Domain.OfferAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Offers.Commands.DeleteOffer
{
    public class DeleteOfferCommandHandler : IRequestHandler<DeleteOfferCommand, ErrorOr<Unit>>
    {
        private readonly IOfferRepository _offerRepository;

        public DeleteOfferCommandHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<ErrorOr<Unit>> Handle(DeleteOfferCommand command, CancellationToken cancellationToken)
        {
            var offerId = OfferId.Create(command.OfferId);

            if (await _offerRepository.GetByIdAsync(offerId) is not Offer offer)
                return Errors.Offer.NotFound;

            var employeeId = EmployeeId.Create(command.EmployeeId);

            bool isAdmin = employeeId.Value == Guid.Empty;

            if (!isAdmin && offer.EmployeeId != employeeId)
                return Errors.User.NoPermissions;

            await _offerRepository.DeleteOfferAsync(offer);

            return Unit.Value;
        }
    }
}
