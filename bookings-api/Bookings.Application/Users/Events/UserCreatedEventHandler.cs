using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.UserAggregate.Events;
using MediatR;

namespace Bookings.Application.Users.Events
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IClientRepository _clientRepository;

        public UserCreatedEventHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var userId = notification.user.Id;

            var client = Client.CreateUnique(userId);

            await _clientRepository.AddAsync(client);
        }
    }
}
