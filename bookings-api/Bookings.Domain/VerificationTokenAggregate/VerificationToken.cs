using Bookings.Domain.Common.Models;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;
using Bookings.Domain.VerificationTokenAggregate.Enums;
using Bookings.Domain.VerificationTokenAggregate.Events;
using Bookings.Domain.VerificationTokenAggregate.ValueObjects;

namespace Bookings.Domain.VerificationTokenAggregate
{
    public sealed class VerificationToken : AggregateRoot<VerificationTokenId>
    {
        public Email Email { get; init; }
        public UserId? UserId { get; private set; } 
        public VerificationTokenType Type { get; init; } 
        public DateTime ExpiresAt { get; init; }
        public DateTime? UsedAt { get; private set; }
        public bool IsExpired => ExpiresAt < DateTime.UtcNow;
        public bool IsUsed => UsedAt is not null;

        private VerificationToken(
            VerificationTokenId id,
            Email email,
            UserId? userId,
            VerificationTokenType type,
            DateTime expiresAt)
            : base(id)
        {
            Email = email;
            UserId = userId;
            Type = type;
            ExpiresAt = expiresAt;
        }

        public static VerificationToken Create(
            string email,
            UserId? userId,
            VerificationTokenType type,
            TimeSpan validity)
        {
            var token = new VerificationToken(
                VerificationTokenId.CreateUnique(),
                Email.Create(email),
                userId,
                type,
                DateTime.UtcNow.Add(validity));

            token.AddDomainEvent(new VerificationTokenCreatedEvent(token));

            return token;
        }

        public void MarkAsUsed(UserId userId)
        {
            if (IsUsed) throw new InvalidOperationException("Token already used.");
            if (DateTime.UtcNow > ExpiresAt) throw new InvalidOperationException("Token expired.");

            UserId ??= userId;
            UsedAt = DateTime.UtcNow;
        }

#pragma warning disable CS8618
        private VerificationToken()
        {
        }
#pragma warning restore CS8618
    }
}
