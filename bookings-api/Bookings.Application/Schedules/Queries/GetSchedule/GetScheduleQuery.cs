using Bookings.Application.Schedules.Common;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Schedules.Queries.GetSchedule
{
    public record GetScheduleQuery(
        string EmployeeId,
        int? Days = null,
        DateOnly? From = null,
        DateOnly? To = null) : IRequest<ErrorOr<IEnumerable<DayScheduleResultDto>>>;
}
