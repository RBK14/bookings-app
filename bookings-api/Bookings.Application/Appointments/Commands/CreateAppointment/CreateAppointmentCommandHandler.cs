using Bookings.Application.Appointments.Commands.CreateAppointment;
using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.OfferAggregate;
using Bookings.Domain.OfferAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Offers.Commands.CreateOffer
{
    public class CreateAppointmentCommandHandler(
        IOfferRepository offerRepository,
        IAppointmentRepository appointmentRepository) : IRequestHandler<CreateAppointmentCommand, ErrorOr<Appointment>>
    {
        private readonly IOfferRepository _offerRepository = offerRepository;
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;

        public async Task<ErrorOr<Appointment>> Handle(CreateAppointmentCommand command, CancellationToken cancellationToken)
        {
            var offerId = OfferId.Create(command.OfferId);
            if (await _offerRepository.GetByIdAsync(offerId) is not Offer offer)
                return Errors.Offer.NotFound;

            var clientId = ClientId.Create(command.ClientId);

            var appointment = Appointment.Create(
                offerId,
                offer.Name,
                offer.Price,
                offer.Duration,
                offer.EmployeeId,
                clientId,
                command.StartTime);

            await _appointmentRepository.AddAsync(appointment);

            return appointment;
        }
    }
}
