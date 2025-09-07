using Bookings.Domain.Common.Models;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;
using Bookings.Domain.VerificationTokenAggregate.ValueObjects;

namespace Bookings.Domain.VerificationTokenAggregate
{
    public sealed class VerificationToken : Entity<VerificationTokenId>
    {
        public Email Email { get; init; }
        public UserId? UserId { get; private set; } 
        public DateTime ExpiresAt { get; init; }
        public DateTime? UsedAt { get; private set; }
        public bool IsExpired => ExpiresAt < DateTime.UtcNow;
        public bool IsUsed => UsedAt is not null;

        private VerificationToken(
            VerificationTokenId id,
            Email email,
            UserId? userId,
            DateTime expiresAt)
            : base(id)
        {
            Email = email;
            UserId = userId;
            ExpiresAt = expiresAt;
        }

        public static VerificationToken Create(
            string email,
            TimeSpan validity,
            UserId? userId = null)
        {
            return new VerificationToken(
                VerificationTokenId.CreateUnique(),
                Email.Create(email),
                userId,
                DateTime.UtcNow.Add(validity));
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
