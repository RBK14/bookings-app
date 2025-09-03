using Bookings.Domain.EmployeeAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Employees.Queries
{
    public record GetEmployeeQuery(string Id) : IRequest<ErrorOr<Employee>>;
}
