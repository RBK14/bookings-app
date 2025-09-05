using Bookings.Application.Schedules.Common;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Schedules.Queries.GetSchedule
{
    public class GetScheduleQueryHandler : IRequestHandler<GetScheduleQuery, ErrorOr<IEnumerable<WorkHoursResultDto>>>
    {
        public Task<ErrorOr<IEnumerable<WorkHoursResultDto>>> Handle(GetScheduleQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
