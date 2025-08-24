using Bookings.Application.Authentication.Commands.Register;
using Bookings.Application.Authentication.Common;
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
            
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}
