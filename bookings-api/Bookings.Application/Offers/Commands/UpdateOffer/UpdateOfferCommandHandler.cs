using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Enums;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate;
using Bookings.Domain.OfferAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Offers.Commands.UpdateOffer
{
    public class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand, ErrorOr<Offer>>
{
        private readonly IOfferRepository _offerRepository;

        public UpdateOfferCommandHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<ErrorOr<Offer>> Handle(UpdateOfferCommand command, CancellationToken cancellationToken)
        {
            var offerId = OfferId.Create(command.OfferId);

            if (await _offerRepository.GetByIdAsync(offerId) is not Offer offer)
                return Errors.Offer.NotFound;

            var employeeId = EmployeeId.Create(command.EmployeeId);

            bool isAdmin = employeeId.Value == Guid.Empty;

            if (!isAdmin && offer.EmployeeId != employeeId)
                return Errors.User.NoPermissions;

            var currency = CurrencyExtensions.FromCode(command.Currency);
            var duration = Duration.Parse(command.Duration);

            offer.UpdateName(command.Name);
            offer.UpdateDescription(command.Description);
            offer.UpdatePrice(command.Amount, currency);
            offer.UpdateDuration(duration);

            await _offerRepository.UpdateAsync(offer);

            return offer;
        }
    }
}
