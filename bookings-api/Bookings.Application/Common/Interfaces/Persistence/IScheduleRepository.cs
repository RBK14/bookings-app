using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.ScheduleAggregate;
using Bookings.Domain.ScheduleAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IScheduleRepository : IBaseRepository
    {
        void Add(Schedule schedule);
        Task<Schedule?> GetByIdAsync(ScheduleId scheduleId);
        Task<Schedule?> GetByEmployeeIdAsync(EmployeeId employeeId);
        void Update(Schedule schedule);
        void Delete(Schedule schedule);
    }
}
