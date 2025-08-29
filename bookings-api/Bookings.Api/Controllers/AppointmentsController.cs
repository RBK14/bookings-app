using Bookings.Application.Appointments.Commands.CreateAppointment;
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
    }
}
