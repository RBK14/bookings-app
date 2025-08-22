using Bookings.Application.Common.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bookings.Application.Common.Interfaces.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public AuthenticationResult Login(string email, string password)
        {
            // Validate credential

            // Load user from database
            Guid userId = Guid.NewGuid();

            // Generate JWT token
            var token = _jwtTokenGenerator.GenerateToken(userId, "name");

            return new AuthenticationResult(
                userId,
                "name",
                "phone",
                email,
                "token");
        }

        public AuthenticationResult Register(string name, string phone, string email, string password)
        {
            // Check if user already exists

            // Create user (generate unique ID)
            Guid userId = Guid.NewGuid();

            // Generate JWT token
            var token = _jwtTokenGenerator.GenerateToken(userId, name);

            return new AuthenticationResult(
                userId,
                name,
                phone,
                email,
                token);
        }
    }
}
