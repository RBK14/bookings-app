using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.ScheduleAggregate;
using Bookings.Domain.ScheduleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class ScheduleRepository(BookingsDbContext dbContext) : IScheduleRepository
    {
        private readonly BookingsDbContext _dbContext = dbContext;

        public async Task AddAsync(Schedule schedule)
        {
            _dbContext.Schedules.Add(schedule);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Schedule?> GetByIdAsync(ScheduleId scheduleId)
        {
            return await _dbContext.Schedules
                .Include(s => s.DefaultSchedules)
                .Include(s => s.Overrides)
                .Include(s => s.Slots)
                .SingleOrDefaultAsync(s => s.Id == scheduleId);
        }

        public async Task<Schedule?> GetByEmployeeIdAsync(EmployeeId employeeId)
        {
            return await _dbContext.Schedules
                .Include(s => s.DefaultSchedules)
                .Include(s => s.Overrides)
                .Include(s => s.Slots)
                .SingleOrDefaultAsync(s => s.EmployeeId == employeeId);
        }

        public async Task UpdateAsync(Schedule schedule)
        {
            _dbContext.Schedules.Update(schedule);
            //await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Schedule schedule)
        {
            _dbContext.Schedules.Remove(schedule);
            await _dbContext.SaveChangesAsync();
        }
    }
}
