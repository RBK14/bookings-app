using Bookings.Contracts.Clients;
using Bookings.Domain.ClientAggregate;
using Mapster;

namespace Bookings.Api.Common.Mapping
{
    public class ClientMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Client, ClientResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.UserId, src => src.UserId.Value)
                .Map(dest => dest.AppointmentIds, src => src.AppointmentIds.Select(appointmentId => appointmentId.Value));
        }
    }
}
