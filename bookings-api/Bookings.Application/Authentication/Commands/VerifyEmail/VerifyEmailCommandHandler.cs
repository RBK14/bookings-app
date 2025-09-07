using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.VerificationTokenAggregate;
using Bookings.Domain.VerificationTokenAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler(
        IVerificationTokenRepository verificationTokenRepository,
        IUserRepository userRepository) : IRequestHandler<VerifyEmailCommand, ErrorOr<Unit>>
    {
        private readonly IVerificationTokenRepository _verificationTokenRepository = verificationTokenRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ErrorOr<Unit>> Handle(VerifyEmailCommand command, CancellationToken cancellationToken)
        {
            var tokenId = VerificationTokenId.Create(command.TokenId);
            if (await _verificationTokenRepository.GetById(tokenId) is not VerificationToken token)
                return Errors.Authentication.InvalidVerificationToken;

            if (token.IsExpired)
                return Errors.Authentication.VerificationTokenExpired;

            if (token.IsUsed)
                return Errors.Authentication.VerificationTokenUsed;

            var userId = token.UserId;
            if (await _userRepository.GetByIdAsync(userId!) is not User user)
                return Errors.User.NotFound;

            token.MarkAsUsed(userId!);
            user.VerifyEmail(tokenId);
            
            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
