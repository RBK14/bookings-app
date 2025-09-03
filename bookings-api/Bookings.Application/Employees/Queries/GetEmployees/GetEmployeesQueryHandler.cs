using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.EmployeeAggregate;
using MediatR;

namespace Bookings.Application.Employees.Queries.GetEmployees
{
    public class GetEmployeesQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetEmployeesQuery, IEnumerable<Employee>>
    {
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

        public async Task<IEnumerable<Employee>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync();

            return employees;
        }
    }
}
