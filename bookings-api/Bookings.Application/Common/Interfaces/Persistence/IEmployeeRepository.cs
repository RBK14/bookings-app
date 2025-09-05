using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IEmployeeRepository : IBaseRepository
    {
        void Add(Employee employee);
        Task<Employee?> GetByIdAsync(EmployeeId employeeId);
        Task<Employee?> GetByUserIdAsync(UserId userId);
        Task<IEnumerable<Employee>> GetAllAsync();
        void Update(Employee employee);
        void Delete(Employee employee);
    }
}
