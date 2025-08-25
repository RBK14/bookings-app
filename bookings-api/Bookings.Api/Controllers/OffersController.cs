using Bookings.Contracts.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class OffersController : ApiController
    {
        [HttpGet]
        public IActionResult ListOffers()
        {
            return Ok(Array.Empty<string>());
        }
    }
}
