using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository(BookingsDbContext dbContext) : BaseRepository(dbContext), IAppointmentRepository
    {
        public void Add(Appointment appointment)
        {
            _dbContext.Appointments.AddAsync(appointment);
        }

        public async Task<Appointment?> GetByIdAsync(AppointmentId appointmentId)
        {
            return await _dbContext.Appointments.FindAsync(appointmentId);
        }

        public async Task<IEnumerable<Appointment>> SearchAsync(
            IEnumerable<IFilterable<Appointment>>? filters,
            ISortable<Appointment>? sort)
        {
            var appointmentsQuery = _dbContext.Appointments.AsQueryable();

            if (filters is not null)
                foreach (var filter in filters)
                    appointmentsQuery = filter.Apply(appointmentsQuery);

            if (sort is not null)
                appointmentsQuery = sort.Apply(appointmentsQuery);

            return await appointmentsQuery.ToListAsync();
        }

        public void Update(Appointment appointment)
        {
            _dbContext.Appointments.Update(appointment);
        }

        public void Delete(Appointment appointment)
        {
            _dbContext.Appointments.Remove(appointment);
        }
    }
}
