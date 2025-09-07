using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.AppointmentAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IAppointmentRepository : IBaseRepository
    {
        void Add(Appointment appointment);
        Task<Appointment?> GetByIdAsync(AppointmentId appointmentId);
        Task<IEnumerable<Appointment>> SearchAsync(IEnumerable<IFilterable<Appointment>>? filters = default, ISortable<Appointment>? sort = default);
        void Update(Appointment appointment);
        void Delete(Appointment appointment);
    }
}