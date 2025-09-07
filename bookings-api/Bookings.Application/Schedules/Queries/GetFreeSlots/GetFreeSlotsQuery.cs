using Bookings.Application.Schedules.Common;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Schedules.Queries.GetFreeSlots
{
    public record GetFreeSlotsQuery(
        string OfferId,
        DateOnly? From = null,
        int Days = 14) : IRequest<ErrorOr<IEnumerable<FreeDaySlotsResultDto>>>;
}
