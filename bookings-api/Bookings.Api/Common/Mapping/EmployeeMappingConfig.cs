using Bookings.Contracts.Employees;
using Bookings.Domain.EmployeeAggregate;
using Mapster;

namespace Bookings.Api.Common.Mapping
{
    public class EmployeeMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Employee, EmployeeResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.UserId, src => src.Id.Value)
                .Map(dest => dest.OfferIds, src => src.OfferIds.Select(offerId => offerId.Value))
                .Map(dest => dest.AppointmentIds, src => src.AppointmentIds.Select(appointmentId => appointmentId.Value));
        }
    }
}
