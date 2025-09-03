using Bookings.Application.Authentication.Commands.Register;
using Bookings.Application.Authentication.Commands.UpdatePassword;
using Bookings.Application.Authentication.Queries.Login;
using Bookings.Contracts.Authentication;
using Mapster;

namespace Bookings.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<(UpdatePasswordRequest Request, string UserId), UpdatePasswordCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.Request);
        }
    }
}
