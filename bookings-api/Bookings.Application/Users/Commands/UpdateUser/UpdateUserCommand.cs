using Bookings.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(
        string UserId,
        string FirstName,
        string LastName,
        string Email,
        string Phone) : IRequest<ErrorOr<User>>;
}
