using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.ScheduleAggregate;
using Bookings.Domain.ScheduleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class ScheduleRepository(BookingsDbContext dbContext) : BaseRepository(dbContext), IScheduleRepository
    {
        public void Add(Schedule schedule)
        {
            _dbContext.Schedules.Add(schedule);
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

        public void Update(Schedule schedule)
        {
            _dbContext.Schedules.Update(schedule);
        }

        public void Delete(Schedule schedule)
        {
            _dbContext.Schedules.Remove(schedule);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
