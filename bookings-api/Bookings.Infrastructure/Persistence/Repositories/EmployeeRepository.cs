using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository(BookingsDbContext dbContext) : BaseRepository(dbContext), IEmployeeRepository
    {
        public void Add(Employee employee)
        {
            _dbContext.Employees.Add(employee);
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
                .SingleOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees
                .Include(e => e.OfferIds)
                .Include(e => e.AppointmentIds)
                .ToListAsync();
        }

        public void Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
        }

        public void Delete(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
