using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Bookings.Domain.UserAggregate;
using Bookings.Application.Common.Interfaces.Authentication;

namespace Bookings.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<Unit>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<Unit>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetUserByEmailAsync(command.Email) is not null)
                return Errors.User.DuplicateEmail;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

            var user = User.CreateUnique(
                command.FirstName,
                command.LastName,
                command.Email,
                passwordHash,
                command.Phone);

            await _userRepository.AddAsync(user);

            var token = _jwtTokenGenerator.GenerateToken(user);

            return Unit.Value;
        }
    }
}
