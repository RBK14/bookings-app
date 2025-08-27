using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Enums;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Offers.Commands.CreateOffer
{
    public class CreateOfferCommandHandler : IRequestHandler<CreateOfferCommand, ErrorOr<Offer>>
    {
        private readonly IOfferRepository _offerRepository;

        public CreateOfferCommandHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public async Task<ErrorOr<Offer>> Handle(CreateOfferCommand command, CancellationToken cancellationToken)
        {
            var employeeId = EmployeeId.Create(command.EmployeeId);
            var currency = CurrencyExtensions.FromCode(command.Currency);
            var duration = Duration.Parse(command.Duration);

            var offer = Offer.Create(
                command.Name,
                command.Description,
                employeeId,
                command.Amount,
                currency,
                duration);

            await _offerRepository.AddAsync(offer);

            return offer;
        }
    }
}
