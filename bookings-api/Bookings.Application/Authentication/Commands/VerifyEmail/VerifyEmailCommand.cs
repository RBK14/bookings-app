using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.VerifyEmail
{
    public record VerifyEmailCommand(string TokenId) : IRequest<ErrorOr<Unit>>;
}
