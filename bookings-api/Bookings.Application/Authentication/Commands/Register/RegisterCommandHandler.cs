using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Common.Interfaces.Services;
using Bookings.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Bookings.Application.Authentication.Common;
using Bookings.Domain.UserAggregate;

namespace Bookings.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetUserByEmailAsync(command.Email) is not null)
                return Errors.User.DuplicateEmail;

            var user = User.CreateUnique(
                command.FirstName,
                command.LastName,
                command.Email,
                command.Password,
                command.Phone);

            await _userRepository.AddAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
