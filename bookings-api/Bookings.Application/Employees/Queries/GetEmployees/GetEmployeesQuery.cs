using Bookings.Domain.EmployeeAggregate;
using MediatR;

namespace Bookings.Application.Employees.Queries.GetEmployees
{
    public record GetEmployeesQuery() : IRequest<IEnumerable<Employee>>;
}
