using Bookings.Application.Services.Authentication;
using Bookings.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _authenticationService.Register(
                request.Name,
                request.Phone,
                request.Email,
                request.Password);

            var response = new AuthenticationResponse(
                authResult.Id,
                authResult.Name,
                authResult.Phone,
                authResult.Email,
                authResult.Token);

            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login(
                request.Email,
                request.Password);

            var response = new AuthenticationResponse(
                authResult.Id,
                authResult.Name,
                authResult.Phone,
                authResult.Email,
                authResult.Token);

            return Ok(response);
        }
    }
}
