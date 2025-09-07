using Bookings.Application.Common.Interfaces.Authentication;
using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.RegisterClient
{
    public class RegisterClientCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : IRequestHandler<RegisterClientCommand, ErrorOr<Unit>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<ErrorOr<Unit>> Handle(RegisterClientCommand command, CancellationToken cancellationToken)
        {
            var email = Email.Create(command.Email);
            if (await _userRepository.GetByEmailAsync(email) is not null)
                return Errors.User.DuplicateEmail;

            var passwordHash = _passwordHasher.HashPassword(command.Password);

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
