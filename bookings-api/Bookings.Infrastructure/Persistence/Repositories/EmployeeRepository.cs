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
            return await _dbContext.Employees
                .Include(e => e.OfferIds)
                .Include(e => e.AppointmentIds)
                .SingleOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<Employee?> GetByUserIdAsync(UserId userId)
        {
            return await _dbContext.Employees
                .Include(e => e.OfferIds)
                .Include(e => e.AppointmentIds)
                .SingleOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees
                .Include(e => e.OfferIds)
                .Include(e => e.AppointmentIds)
                .ToListAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            //await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
