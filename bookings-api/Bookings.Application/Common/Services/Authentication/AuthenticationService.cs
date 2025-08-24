using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Common.Interfaces.Services;
using Bookings.Domain.Entities;
using Bookings.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bookings.Application.Common.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            // Validate the user exists
            if (_userRepository.GetUserByEmail(email) is not User user)
                throw new Exception("Invalid email or password");

            // Validate the password is correct
            if (user.Password != password)
                throw new Exception("Invalid email or password");

            // Generate JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }

        public AuthenticationResult Register(string name, string phone, string email, string password)
        {
            // Validate the user doesn't exists
            if (_userRepository.GetUserByEmail(email) is not null)
                throw new Exception("User with given email already exists");

            // Create user & persist to the DB
            var user = new User(name, phone, email, password);
            _userRepository.Add(user);
            
            // Generate JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
