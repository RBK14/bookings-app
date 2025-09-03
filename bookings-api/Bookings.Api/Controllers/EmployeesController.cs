using Bookings.Application.Employees.Queries;
using Bookings.Application.Employees.Queries.GetEmployees;
using Bookings.Contracts.Employees;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Api.Controllers
{
    [Route("api/employees")]
    public class EmployeesController(ISender mediator, IMapper mapper) : ApiController
    {
        private readonly ISender _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var query = new GetEmployeesQuery();

            var result = await _mediator.Send(query);

            var response = result
                .Select(e => _mapper.Map<EmployeeResponse>(e))
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var query = new GetEmployeeQuery(id);

            var result = await _mediator.Send(query);

            return result.Match(
                employee => Ok(_mapper.Map<EmployeeResponse>(employee)),
                errors => Problem(errors)
                );
        }
    }
}
