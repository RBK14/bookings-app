using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.ScheduleAggregate;
using Bookings.Domain.ScheduleAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IScheduleRepository
    {
        Task AddAsync(Schedule schedule);
        Task<Schedule?> GetByIdAsync(ScheduleId scheduleId);
        Task<Schedule?> GetByEmployeeIdAsync(EmployeeId employeeId);
        Task UpdateAsync(Schedule schedule);
        Task DeleteAsync(Schedule schedule);
    }
}
