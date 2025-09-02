using Bookings.Application.Appointments.Commands.CreateAppointment;
using Bookings.Application.Appointments.Commands.UpdateAppointment;
using Bookings.Contracts.Appointments;
using Bookings.Domain.AppointmentAggregate;
using Mapster;

namespace Bookings.Api.Common.Mapping
{
    public class AppointmentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CreateAppointmentRequest Request, string ClientId), CreateAppointmentCommand>()
                .Map(dest => dest.ClientId, src => src.ClientId)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<(UpdateAppointmentRequest Request, string AppointmentId, string EmployeeId), UpdateAppointmentCommand>()
                .Map(dest => dest.AppointmentId, src => src.AppointmentId)
                .Map(dest => dest.EmployeeId, src => src.EmployeeId)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<Appointment, AppointmentResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.OfferId, src => src.OfferId.Value)
                .Map(dest => dest.OfferAmount, src => src.OfferPrice.Amount)
                .Map(dest => dest.OfferCurrency, src => src.OfferPrice.Currency)
                .Map(dest => dest.OfferDuration, src => src.OfferDuration.Value)
                .Map(dest => dest.EmployeeId, src => src.EmployeeId.Value)
                .Map(dest => dest.ClientId, src => src.ClientId.Value)
                .Map(dest => dest.StartTime, src => src.Time.Start)
                .Map(dest => dest.EndTime, src => src.Time.End);
        }
    }
}
