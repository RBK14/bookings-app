using Bookings.Application.Authentication.Commands.CreateEmployeeInvitation;
using Bookings.Application.Authentication.Commands.RegisterClient;
using Bookings.Application.Authentication.Commands.RegisterEmployee;
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
            config.NewConfig<RegisterRequest, RegisterClientCommand>();

            config.NewConfig<(RegisterRequest Request, string TokenId), RegisterEmployeeCommand>()
                .Map(dest => dest.TokenId, src => src.TokenId)
                .Map(dest => dest, src => src.Request);

            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<CreateEmployeeInvitationRequest, CreateEmployeeInvitationCommand>();

            config.NewConfig<(UpdatePasswordRequest Request, string UserId), UpdatePasswordCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest, src => src.Request);
        }
    }
}
