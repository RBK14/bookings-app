using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string Phone) : IRequest<ErrorOr<Unit>>;
}
