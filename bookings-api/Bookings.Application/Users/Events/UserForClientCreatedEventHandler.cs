using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.UserAggregate.Events;
using Bookings.Domain.VerificationTokenAggregate;
using Bookings.Domain.VerificationTokenAggregate.Enums;
using MediatR;

namespace Bookings.Application.Users.Events
{
    public class UserForClientCreatedEventHandler(
        IVerificationTokenRepository verificationTokenRepository,
        IClientRepository clientRepository) : INotificationHandler<UserForClientCreatedEvent>
    {
        private readonly IVerificationTokenRepository _verificationTokenRepository = verificationTokenRepository;
        private readonly IClientRepository _clientRepository = clientRepository;

        public async Task Handle(UserForClientCreatedEvent notification, CancellationToken cancellationToken)
        {
            var user = notification.User;
            var userId = notification.User.Id;
            var userEmail = notification.User.Email.Value;
            var tokenType = VerificationTokenType.EmailVerification;
            var tokenValidity = TimeSpan.FromMinutes(30);

            var verificationToken = VerificationToken.Create(userEmail, userId, tokenType, tokenValidity);

            user.AddVerificationTokenId(verificationToken.Id);
            _verificationTokenRepository.Add(verificationToken);

            var existingClient = await _clientRepository.GetByUserIdAsync(userId);
            if (existingClient is not null)
                return;

            var client = Client.Create(userId);

            _clientRepository.Add(client);
        }
    }
}
