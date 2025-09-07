using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.RegisterClient
{
    public record RegisterClientCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string ConfirmPassword,
        string Phone) : IRequest<ErrorOr<Unit>>;
}
