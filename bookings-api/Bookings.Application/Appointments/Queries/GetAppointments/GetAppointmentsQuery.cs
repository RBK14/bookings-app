using Bookings.Domain.AppointmentAggregate;
using MediatR;

namespace Bookings.Application.Appointments.Queries.GetAppointments
{
    public record GetAppointmentsQuery(
        string? Name,
        decimal? MinPrice,
        decimal? MaxPrice,
        int? Currency,
        string? MinDuration,
        string? MaxDuration,
        string? EmployeeId,
        string? ClientId,
        DateTime? Starts,
        DateTime? Ends,
        string? Status,
        string? SortBy) : IRequest<IEnumerable<Appointment>>;
}
