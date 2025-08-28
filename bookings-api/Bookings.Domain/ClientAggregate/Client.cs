using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;

namespace Bookings.Domain.ClientAggregate
{
    public class Client : AggregateRoot<ClientId>
    {
        private readonly List<AppointmentId> _appointmentIds = new();

        public UserId UserId { get; init; }
        public IReadOnlyList<AppointmentId> AppointmentIds => _appointmentIds.AsReadOnly();
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; private set; }

        private Client(
            ClientId clientId,
            UserId userId,
            DateTime createdAt,
            DateTime updatedAt) : base(clientId)
        {
            UserId = userId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static Client CreateUnique(UserId userId)
        {
            return new Client(
                ClientId.CrateUnique(),
                userId,
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

#pragma warning disable CS8618
        private Client()
        {
        }
#pragma warning restore CS8618
    }
}
