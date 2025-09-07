using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.RegisterEmployee
{
    public record RegisterEmployeeCommand(
        string TokenId,
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string ConfirmPassword,
        string Phone) : IRequest<ErrorOr<Unit>>;
}
