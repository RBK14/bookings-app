using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.RegisterEmployee
{
    public record RegisterEmployeeCommand(
        string TokenId,
        string FirstName,
        string LastName,
        string Email,
        string Phone,
        string Password,
        string ConfirmPassword)
        : IRequest<ErrorOr<Unit>>;
}
