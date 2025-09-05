using Bookings.Application.Schedules.Commands.SetDefaultSchedule;
using Bookings.Application.Schedules.Commands.SetScheduleOverride;
using Bookings.Application.Schedules.Queries.GetWorkHours;
using Bookings.Contracts.Schedule;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Api.Controllers
{
    [Route("api/schedules")]
    [ApiController]
    public class SchedulesController(ISender mediator, IMapper mapper) : ApiController
    {
        private readonly ISender _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpPost("default")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> SetDefaultSchedule(SetDefaultScheduleRequest request)
        {
            var employeeId = User.FindFirst("RoleId")?.Value;

            if (string.IsNullOrWhiteSpace(employeeId))
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: "Identyfikator pracownika jest nieprawidłowy."
                );

            var command = _mapper.Map<SetDefaultScheduleCommand>((request, employeeId)); 

            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => Problem(errors)
            );
        }

        [HttpPost("override")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> SetScheduleOverride(SetScheduleOverrideRequest request)
        {
            var employeeId = User.FindFirst("RoleId")?.Value;

            if (string.IsNullOrWhiteSpace(employeeId))
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: "Identyfikator pracownika jest nieprawidłowy."
                );

            var command = _mapper.Map<SetScheduleOverrideCommand>((request, employeeId));

            var result = await _mediator.Send(command);
            
            return result.Match(
                _ => Ok(),
                errors => Problem(errors)
            );
        }

        [HttpGet("workhours")]
        public async Task<IActionResult> GetWorkHours(
            [FromQuery] string employeeId,
            [FromQuery] int? days,
            [FromQuery] DateOnly? from,
            [FromQuery] DateOnly? to)
        {
            var query = new GetWorkHoursQuery(employeeId, days, from, to);

            var result = await _mediator.Send(query);

            return result.Match(
                workHours =>
                {
                    var response = workHours.Select(w => _mapper.Map<WorkHoursResponse>(w)).ToList();
                    return Ok(response);
                },
                errors => Problem(errors)
            );
        }
    }
}
