using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.UpdatePassword
{
    public record UpdatePasswordCommand(
        string UserId,
        string CurrentPassword,
        string NewPassword,
        string ConfirmNewPassword) : IRequest<ErrorOr<Unit>>;
}
