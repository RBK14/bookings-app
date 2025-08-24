using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Common.Interfaces.Services;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.Entities;
using Bookings.Domain.Enums;
using ErrorOr;
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

        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            // Validate the user exists
            if (_userRepository.GetUserByEmail(email) is not User user)
                return Errors.Authentication.InvalidCredentials;

            // Validate the password is correct
            if (user.Password != password)
                return Errors.Authentication.InvalidCredentials;

            // Generate JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }

        public ErrorOr<AuthenticationResult> Register(string name, string phone, string email, string password)
        {
            // Validate the user doesn't exists
            if (_userRepository.GetUserByEmail(email) is not null)
                return Errors.User.DuplicateEmail;

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
