using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.CreateEmployeeInvitation
{
    public record CreateEmployeeInvitationCommand(string Email) : IRequest<ErrorOr<Unit>>;
}
