using Bookings.Application.Common.Interfaces.Authentication;
using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.Enums;
using Bookings.Domain.VerificationTokenAggregate;
using Bookings.Domain.VerificationTokenAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.RegisterEmployee
{
    public class RegisterEmployeeCommandHandler(
        IVerificationTokenRepository verificationTokenRepository,
        IUserRepository userRepository,
        IPasswordHasher passwordHasher) : IRequestHandler<RegisterEmployeeCommand, ErrorOr<Unit>>
    {
        private readonly IVerificationTokenRepository _verificationTokenRepository = verificationTokenRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<ErrorOr<Unit>> Handle(RegisterEmployeeCommand command, CancellationToken cancellationToken)
        {
            var email = Email.Create(command.Email);

            var tokenId = VerificationTokenId.Create(command.TokenId);
            if (await _verificationTokenRepository.GetByIdAsync(tokenId) is not VerificationToken token)
                return Errors.Authentication.InvalidVerificationToken;

            if (token.IsExpired)
                return Errors.Authentication.VerificationTokenExpired;

            if (token.IsUsed)
                return Errors.Authentication.VerificationTokenUsed;

            if (email != token.Email)
                return Errors.Authentication.InvalidEmail;

            if (await _userRepository.GetByEmailAsync(email) is not null)
                return Errors.User.DuplicateEmail;

            var passwordHash = _passwordHasher.HashPassword(command.Password);

            var user = User.CreateUnique(
                command.FirstName,
                command.LastName,
                command.Email,
                passwordHash,
                command.Phone,
                UserRole.Employee,
                true);

            user.AddVerificationTokenId(tokenId);
            token.MarkAsUsed(user.Id);
            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}