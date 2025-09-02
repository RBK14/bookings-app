using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository(BookingsDbContext dbContext) : IAppointmentRepository
    {
        private readonly BookingsDbContext _dbContext = dbContext;

        public async Task AddAsync(Appointment appointment)
        {
            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Appointment?> GetByIdAsync(AppointmentId appointmentId)
        {
            return await _dbContext.Appointments.FindAsync(appointmentId);
        }

        public async Task<IEnumerable<Appointment>> SearchAsync(
            IEnumerable<IFilterable<Appointment>>? filters,
            ISortable<Appointment>? sort)
        {
            var appointments = await _dbContext.Appointments.ToListAsync();

            var appointmentsQuery = appointments.AsQueryable();

            if (filters is not null)
                foreach (var filter in filters)
                {
                    appointmentsQuery = filter.Apply(appointmentsQuery);
                }

            if (sort is not null)
                appointmentsQuery = sort.Apply(appointmentsQuery);

            return appointmentsQuery;
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Appointment appointment)
        {
            _dbContext.Appointments.Remove(appointment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
