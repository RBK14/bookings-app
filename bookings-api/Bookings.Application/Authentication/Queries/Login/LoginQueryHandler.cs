using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Common.Interfaces.Services;
using Bookings.Application.Authentication.Common;
using Bookings.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Bookings.Domain.UserAggregate;

namespace Bookings.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // Validate the user exists
            if (_userRepository.GetUserByEmail(query.Email) is not User user)
                return Errors.Authentication.InvalidCredentials;

            // Validate the password is correct
            if (user.Password != query.Password)
                return Errors.Authentication.InvalidCredentials;

            // Generate JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
