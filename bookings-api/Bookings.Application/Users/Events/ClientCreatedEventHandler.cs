using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.UserAggregate.Events;
using Bookings.Domain.VerificationTokenAggregate;
using MediatR;

namespace Bookings.Application.Users.Events
{
    public class ClientCreatedEventHandler(
        IVerificationTokenRepository verificationTokenRepository,
        IClientRepository clientRepository) : INotificationHandler<ClientCreatedEvent>
    {
        private readonly IVerificationTokenRepository _verificationTokenRepository = verificationTokenRepository;
        private readonly IClientRepository _clientRepository = clientRepository;

        public async Task Handle(ClientCreatedEvent notification, CancellationToken cancellationToken)
        {
            var user = notification.User;
            var userId = notification.User.Id;
            var userEmail = notification.User.Email.Value;
            var tokenValidity = TimeSpan.FromMinutes(30);

            var verificationToken = VerificationToken.Create(userEmail, tokenValidity, userId);

            user.AddVerificationTokenId(verificationToken.Id);
            _verificationTokenRepository.Add(verificationToken);

            // TODO: Send email with verification link

            var existingClient = await _clientRepository.GetByUserIdAsync(userId);
            if (existingClient is not null)
                return;

            var client = Client.Create(userId);
            _clientRepository.Add(client);
        }
    }
}
