using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.UserAggregate.Enums;
using Bookings.Domain.UserAggregate.Events;
using Bookings.Domain.UserAggregate.ValueObjects;
using Bookings.Domain.VerificationTokenAggregate.ValueObjects;

namespace Bookings.Domain.UserAggregate
{
    public class User : AggregateRoot<UserId>
    {
        private readonly List<VerificationTokenId> _verificationTokenIds = new();

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public string PasswordHash { get; private set; }
        public Phone Phone { get; private set; }
        public UserRole Role { get; init; }
        public bool IsEmailVerified { get; private set; }
        public IReadOnlyList<VerificationTokenId> VerificationTokenIds => _verificationTokenIds.AsReadOnly();
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; private set; }

        private User(
            UserId userId,
            string firstName,
            string lastName,
            Email email,
            string passwordHash,
            Phone phone,
            UserRole role,
            bool isEmailVerified,
            DateTime createdAt,
            DateTime updatedAt) : base(userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            Phone = phone;
            Role = role;
            IsEmailVerified = isEmailVerified;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static User CreateUnique(
            string firstName,
            string lastName,
            string email,
            string passwordHash,
            string phone,
            UserRole role = UserRole.Client,
            bool isEmailVerified = false)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("FirstName cannot be empty.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("LastName cannot be empty.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("PasswordHash cannot be empty.");

            var user = new User(
                UserId.CreateUnique(),
                firstName,
                lastName,
                Email.Create(email),
                passwordHash,
                Phone.Create(phone),
                role,
                isEmailVerified,
                DateTime.UtcNow,
                DateTime.UtcNow);

            if (role == UserRole.Client)
                user.AddDomainEvent(new UserForClientCreatedEvent(user));

            if (role == UserRole.Employee)
                user.AddDomainEvent(new UserForEmployeeCreatedEvent(user));

            return user;
        }

        public User UpdateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("FirstName cannot be empty.");

            FirstName = firstName;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public User UpdateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("LastName cannot be empty.");

            LastName = lastName;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public User UpdateEmail(string email)
        {
            Email = Email.Create(email);
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public User UpdatePasswordHash(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new DomainException("PasswordHash cannot be empty.");

            PasswordHash = password;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public User UpdatePhone(string phone)
        {
            Phone = Phone.Create(phone);
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

        public void AddVerificationTokenId(VerificationTokenId tokenId)
        {
            if (!_verificationTokenIds.Contains(tokenId))
            {
                _verificationTokenIds.Add(tokenId);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public User VerifyEmail(VerificationTokenId tokenId)
        {
            if (!_verificationTokenIds.Contains(tokenId))
                throw new DomainException("Invalid verification token.");

            IsEmailVerified = true;
            UpdatedAt = DateTime.UtcNow;
            return this;
        }

#pragma warning disable CS8618
        private User()
        {
        }
#pragma warning restore CS8618
    }
}
