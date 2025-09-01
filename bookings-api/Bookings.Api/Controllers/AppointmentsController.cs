using Bookings.Application.Appointments.Commands.CreateAppointment;
using Bookings.Application.Appointments.Commands.DeleteAppointment;
using Bookings.Contracts.Appointments;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Api.Controllers
{
    [Route("appointments")]
    public class AppointmentsController(ISender mediator, IMapper mapper) : ApiController
    {
        private readonly ISender _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetAppoinments(
            [FromQuery] string? name,
            [FromQuery] decimal? maxPrice,
            [FromQuery] decimal? minPrice,
            [FromQuery] int? currency,
            [FromQuery] string? minDuration,
            [FromQuery] string? maxDuration,
            [FromQuery] string? employeeId,
            [FromQuery] string? clientId,
            [FromQuery] DateTime? starts,
            [FromQuery] DateTime? ends,
            [FromQuery] string? status,
            [FromQuery] string? description,
            [FromQuery] string? sortBy)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentRequest request)
        {
            var clientId = User.FindFirst("RoleId")?.Value;

            if (string.IsNullOrEmpty(clientId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: "Identyfikator klienta jest nieprawidłowy.");

            var command = _mapper.Map<CreateAppointmentCommand>((request, clientId));

            var result = await _mediator.Send(command);

            return result.Match(
                appointment => Ok(_mapper.Map<AppointmentResponse>(appointment)),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAppointment(string id)
        {
            var command = new DeleteAppointmentCommand(id);

            var result = await _mediator.Send(command);

            return result.Match(
                r => Ok(),
                errors => Problem(errors));
        }
    }
}
