using ErrorOr;
using MediatR;

namespace Bookings.Application.Schedules.Commands.SetScheduleOverride
{
    public record SetScheduleOverrideCommand(
        string EmployeeId,
        DateOnly Date,
        TimeOnly Start,
        TimeOnly End) : IRequest<ErrorOr<Unit>>;
}
