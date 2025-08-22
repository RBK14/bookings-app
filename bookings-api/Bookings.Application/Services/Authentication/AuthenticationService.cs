using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bookings.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(
                Guid.NewGuid(),
                "name",
                "phone",
                email,
                "token");
        }

        public AuthenticationResult Register(string name, string phone, string email, string password)
        {
            return new AuthenticationResult(
                Guid.NewGuid(),
                name,
                phone,
                email,
                "token");
        }
    }
}
