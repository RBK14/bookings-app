using Bookings.Application.Schedules.Common;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Schedules.Queries.GetWorkHours
{
    public record GetWorkHoursQuery(
        string EmployeeId,
        int? Days = null,
        DateOnly? From = null,
        DateOnly? To = null) : IRequest<ErrorOr<IEnumerable<WorkHoursResultDto>>>;
}
