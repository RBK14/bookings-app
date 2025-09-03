using Bookings.Application.Offers.Commands.CreateOffer;
using Bookings.Application.Offers.Commands.UpdateOffer;
using Bookings.Contracts.Offers;
using Bookings.Domain.OfferAggregate;
using Mapster;

namespace Bookings.Api.Common.Mapping
{
    public class OfferMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CreateOfferRequest Request, string EmployeeId), CreateOfferCommand>()
                .Map(dest => dest.EmployeeId, src => src.EmployeeId)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<(UpdateOfferRequest Request, string OfferId, string EmployeeId), UpdateOfferCommand>()
                .Map(dest => dest.OfferId, src => src.OfferId)
                .Map(dest => dest.EmployeeId, src => src.EmployeeId)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<Offer, OfferResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.EmployeeId, src => src.EmployeeId.Value)
                .Map(dest => dest.Amount, src => src.Price.Amount)
                .Map(dest => dest.Currency, src => src.Price.Currency)
                .Map(dest => dest.Duration, src => src.Duration.Value)
                .Map(dest => dest.AppointmentIds, src => src.AppointmentIds.Select(appointmentId => appointmentId.Value));
        }
    }
}
