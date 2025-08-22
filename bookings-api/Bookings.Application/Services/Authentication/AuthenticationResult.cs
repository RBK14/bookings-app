using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookings.Application.Services.Authentication
{
    public record AuthenticationResult(
        Guid Id,
        string Name,
        string Phone,
        string Email,
        string Token);
}
