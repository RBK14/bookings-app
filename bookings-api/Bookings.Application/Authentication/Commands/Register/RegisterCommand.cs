using Bookings.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string Name,
        string Phone,
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
