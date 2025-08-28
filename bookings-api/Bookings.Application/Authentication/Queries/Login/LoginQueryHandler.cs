using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Authentication.Common;
using Bookings.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Bookings.Domain.UserAggregate;
using Bookings.Application.Common.Interfaces.Authentication;

namespace Bookings.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<string>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<string>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetUserByEmailAsync(query.Email) is not User user)
                return Errors.Authentication.InvalidCredentials;

            if (!BCrypt.Net.BCrypt.Verify(query.Password, user.PasswordHash))
                return Errors.Authentication.InvalidCredentials;

            return _jwtTokenGenerator.GenerateToken(user);
        }
    }
}
