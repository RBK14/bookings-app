using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.AppointmentAggregate.ValueObjects;

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

        public Task<IEnumerable<Appointment>> SearchAsync(IEnumerable<IFilterable<Appointment>>? filters, ISortable<Appointment>? sort)
        {
            throw new NotImplementedException();
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
