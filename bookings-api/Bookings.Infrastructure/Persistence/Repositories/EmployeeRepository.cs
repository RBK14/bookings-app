using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository(BookingsDbContext dbContext) : IEmployeeRepository
    {
        private readonly BookingsDbContext _dbContext = dbContext;

        public async Task AddAsync(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Employee?> GetByIdAsync(EmployeeId employeeId)
        {
            return await _dbContext.Employees.FindAsync(employeeId);
        }

        public async Task<Employee?> GetByUserIdAsync(UserId userId)
        {
            return await _dbContext.Employees.SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public Task<IEnumerable<Employee>> SearchAsync(IEnumerable<IFilterable<Employee>>? filters, ISortable<Employee>? sort)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
