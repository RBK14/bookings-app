using Bookings.Application.Authentication.Commands.CreateEmployeeInvitation;
using Bookings.Application.Authentication.Commands.RegisterClient;
using Bookings.Application.Authentication.Commands.RegisterEmployee;
using Bookings.Application.Authentication.Commands.UpdatePassword;
using Bookings.Application.Authentication.Commands.VerifyEmail;
using Bookings.Application.Authentication.Queries.GetVerificationToken;
using Bookings.Application.Authentication.Queries.Login;
using Bookings.Contracts.Authentication;
using Bookings.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookings.Api.Controllers
{
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthenticationController(ISender mediator, IMapper mapper) : ApiController
    {
        private readonly ISender _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpPost("register-client")]
        public async Task<IActionResult> RegisterClient(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterClientCommand>(request);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => Problem(errors)
            );
        }

        [HttpPost("register-employee/{tokenId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterEmployee(RegisterRequest request, string tokenId)
        {
            var command = _mapper.Map<RegisterEmployeeCommand>((request, tokenId));
            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => Problem(errors)
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            var result = await _mediator.Send(query);

            if (result.IsError && result.FirstError == Errors.Authentication.InvalidCredentials)
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: result.FirstError.Description);

            return result.Match(
               token => Ok(token),
               errors => Problem(errors));
        }

        [HttpPost("verify-email/{tokenId}")]
        public async Task<IActionResult> VerifyEmail(string tokenId)
        {
            var command = new VerifyEmailCommand(tokenId);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => Problem(errors)
            );
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: "Identyfikator użytownika jest nieprawidłowy.");

            var command = _mapper.Map<UpdatePasswordCommand>((request, userId));
            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => Problem(errors));
        }

        [HttpPost("invite-employee")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployeeInvitation(CreateEmployeeInvitationRequest request)
        {
            var command = _mapper.Map<CreateEmployeeInvitationCommand>(request);
            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(),
                errors => Problem(errors)
            );
        }

        [HttpGet("tokens/{id}")]
        public async Task<IActionResult> GetVerificationToken(string id)
        {
            var query = new GetVerificationTokenQuery(id);
            var result = await _mediator.Send(query);

            return result.Match(
                token => Ok(_mapper.Map<VerificationTokenResponse>(token)),
                errors => Problem(errors));
        }
    }
}
