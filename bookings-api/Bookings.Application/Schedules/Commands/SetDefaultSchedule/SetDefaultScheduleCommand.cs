using ErrorOr;
using MediatR;

namespace Bookings.Application.Schedules.Commands.SetDefaultSchedule
{
    public record SetDefaultScheduleCommand(
        string EmployeeId,
        List<WorkDayScheduleCommandDto> Schedules) : IRequest<ErrorOr<Unit>>;

    public record WorkDayScheduleCommandDto(
        DayOfWeek DayOfWeek,
        TimeOnly Start,
        TimeOnly End
    );
}
