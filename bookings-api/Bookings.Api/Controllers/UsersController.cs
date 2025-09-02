using Bookings.Application.Users.Commands.UpdateUser;
using Bookings.Application.Users.Queries.GetUser;
using Bookings.Application.Users.Queries.GetUsers;
using Bookings.Contracts.Appointments;
using Bookings.Contracts.Users;
using Bookings.Domain.UserAggregate.Enums;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookings.Api.Controllers
{
    [Route("users")]
    public class UsersController(ISender mediator, IMapper mapper) : ApiController
    {
        private readonly ISender _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers(
            string? fullName,
            string? role,
            bool? isEmailConfirmed,
            string? sortBy)
        {
            var query = new GetUsersQuery(
                FullName: fullName,
                Role: role,
                IsEmailConfirmed: isEmailConfirmed,
                SortBy: sortBy);

            var result = await _mediator.Send(query);

            var response = result
                .Select(u => _mapper.Map<UserResponse>(u))
                .ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            id = id.ToLower();
            if (string.IsNullOrWhiteSpace(userId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: "Identyfikator użytownika jest nieprawidłowy.");

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            bool isAdmin = Enum.TryParse<UserRole>(userRole, out var role) && role == UserRole.Admin;

            if (id != userId && !isAdmin)
                return Problem(statusCode: StatusCodes.Status403Forbidden, title: "Brak dostępu do zawartości");

            var query = new GetUserQuery(id);

            var result = await _mediator.Send(query);

            return result.Match(
                user => Ok(_mapper.Map<UserResponse>(user)),
                errors => Problem(errors));
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request, string id)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: "Identyfikator użytownika jest nieprawidłowy.");

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            bool isAdmin = Enum.TryParse<UserRole>(userRole, out var role) && role == UserRole.Admin;

            id = id.ToLower();
            if (id != userId && !isAdmin)
                return Problem(statusCode: StatusCodes.Status403Forbidden, title: "Brak dostępu do zawartości");

            var command = _mapper.Map<UpdateUserCommand>((request, id));

            var result = await _mediator.Send(command);

            return result.Match(
                user => Ok(_mapper.Map<UserResponse>(user)),
                errors => Problem(errors));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: "Identyfikator użytownika jest nieprawidłowy.");

            id = id.ToLower();
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            bool isAdmin = Enum.TryParse<UserRole>(userRole, out var role) && role == UserRole.Admin;

            if (id != userId && !isAdmin)
                return Problem(statusCode: StatusCodes.Status403Forbidden, title: "Brak dostępu do zawartości");

            var query = new GetUserQuery(id);

            var result = await _mediator.Send(query);

            return result.Match(
                r => Ok(),
                errors => Problem(errors));
        }
    }
}
