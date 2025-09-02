using Bookings.Application.Users.Commands.UpdateUser;
using Bookings.Contracts.Users;
using Bookings.Domain.UserAggregate;
using Mapster;

namespace Bookings.Api.Common.Mapping
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(UpdateUserRequest Request, string UserId), UpdateUserCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<User, UserResponse>()
                .Map(dest => dest.Id, src => src.Id.Value)
                .Map(dest => dest.Email, src => src.Email.Value)
                .Map(dest => dest.Phone, src => src.Phone.Value)
                .Map(dest => dest, src => src);
        }
    }
}
