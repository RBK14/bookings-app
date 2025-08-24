using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : Controller
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem(title: exception?.Message, statusCode: 400);
    }
}

