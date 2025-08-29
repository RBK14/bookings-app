using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.AppointmentAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment);
        Task<Appointment?> GetByIdAsync(AppointmentId appointmentId);
        Task<IEnumerable<Appointment>> SearchAsync(IEnumerable<IFilterable<Appointment>>? filters, ISortable<Appointment>? sort);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(Appointment appointment);
    }

}
