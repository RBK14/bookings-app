
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.EmployeeAggregate.ValueObjects;

namespace Bookings.Application.Common.Interfaces.Persistence
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);

        Task<Employee> GetById(EmployeeId employeeId);
    }
}
