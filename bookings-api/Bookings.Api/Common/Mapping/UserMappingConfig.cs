using Bookings.Contracts.Users;
using Bookings.Domain.UserAggregate;
using Mapster;

namespace Bookings.Api.Common.Mapping
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Email, src => src.Email.Value)
                .Map(dest => dest.Phone, src => src.Phone.Value)
                .Map(dest => dest, src => src);
        }
    }
}
