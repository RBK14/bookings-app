using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Employees.Queries.GetEmployee
{
    public class GetEmployeeQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetEmployeeQuery, ErrorOr<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<ErrorOr<Employee>> Handle(GetEmployeeQuery query, CancellationToken cancellationToken)
        {
            var employeeId = EmployeeId.Create(query.Id);

            if (await _employeeRepository.GetByIdAsync(employeeId) is not Employee employee)
                return Errors.Employee.NotFound;

            return employee;
        }
    }
}
