using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Bookings.Domain.UserAggregate;
using Bookings.Application.Common.Interfaces.Authentication;
using Bookings.Domain.UserAggregate.ValueObjects;

namespace Bookings.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository) : IRequestHandler<RegisterCommand, ErrorOr<Unit>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ErrorOr<Unit>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var email = Email.Create(command.Email);
            if (await _userRepository.GetByEmailAsync(email) is not null)
                return Errors.User.DuplicateEmail;

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

            var user = User.CreateUnique(
                command.FirstName,
                command.LastName,
                command.Email,
                passwordHash,
                command.Phone);

            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
