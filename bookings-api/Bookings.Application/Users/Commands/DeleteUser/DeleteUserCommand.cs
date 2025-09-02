using ErrorOr;
using MediatR;

namespace Bookings.Application.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(string UserId) : IRequest<ErrorOr<Unit>>;
}
