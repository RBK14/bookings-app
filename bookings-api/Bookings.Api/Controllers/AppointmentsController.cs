using Bookings.Application.Appointments.Commands.CreateAppointment;
using Bookings.Application.Appointments.Commands.DeleteAppointment;
using Bookings.Application.Appointments.Commands.UpdateAppointment;
using Bookings.Application.Appointments.Queries.GetAppointment;
using Bookings.Application.Appointments.Queries.GetAppointments;
using Bookings.Application.Offers.Commands.UpdateOffer;
using Bookings.Contracts.Appointments;
using Bookings.Contracts.Offers;
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
        public async Task<IActionResult> GetAppointments(
            [FromQuery] string? name,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int? currency,
            [FromQuery] string? minDuration,
            [FromQuery] string? maxDuration,
            [FromQuery] string? employeeId,
            [FromQuery] string? clientId,
            [FromQuery] DateTime? starts,
            [FromQuery] DateTime? ends,
            [FromQuery] string? status,
            [FromQuery] string? sortBy)
        {
            var query = new GetAppointmentsQuery(
                Name: name,
                MinPrice: minPrice,
                MaxPrice: maxPrice,
                Currency: currency,
                MinDuration: minDuration,
                MaxDuration: maxDuration,
                EmployeeId: employeeId,
                ClientId: clientId,
                Starts: starts,
                Ends: ends,
                Status: status,
                SortBy: sortBy);

            var result = await _mediator.Send(query);

            var resposne = result
                .Select (a => _mapper.Map<AppointmentResponse>(a))
                .ToList ();

            return Ok(resposne);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(string id)
        {
            var query = new GetAppointmentQuery(id);

            var result = await _mediator.Send(query);

            return result.Match(
                appointment => Ok(_mapper.Map<AppointmentResponse>(appointment)),
                errors => Problem(errors)
            );
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> UpdateAppointment(UpdateAppointmentRequest request, string id)
        {
            var employeeId = User.FindFirst("RoleId")?.Value;

            if (string.IsNullOrEmpty(employeeId))   // If empty => UserRole: Admin
                employeeId = Guid.Empty.ToString(); // Setting employeeId value for admin

            var command = _mapper.Map<UpdateAppointmentCommand>((request, id, employeeId));

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
