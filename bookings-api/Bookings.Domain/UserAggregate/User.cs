using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;

namespace Bookings.Domain.UserAggregate
{
    public class User : AggregateRoot<UserId>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public string PasswordHash { get; private set; }
        public Phone Phone { get; private set; }
        public bool IsEmailConfirmed { get; private set; }
        public string? ConfirmationCode { get; private set; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; private set; }

        private User(
            UserId userId,
            string firstName,
            string lastName,
            Email email,
            string passwordHash,
            Phone phone,
            bool isEmailConfirmed,
            string? confirmationCode,
            DateTime createdAt,
            DateTime updatedAt) : base(userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            Phone = phone;
            IsEmailConfirmed = isEmailConfirmed;
            ConfirmationCode = confirmationCode;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static User CreateUnique(
            string firstName,
            string lastName,
            string email,
            string passwordHash,
            string phone)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("FirstName cannot be empty.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("LastName cannot be empty.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("PasswordHash cannot be empty.");

            return new User(
                UserId.CreateUnique(),
                firstName,
                lastName,
                Email.Create(email),
                passwordHash,
                Phone.Create(phone),
                false,
                null,
                DateTime.UtcNow,
                DateTime.UtcNow);
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

        public User UpdatePassword(string password)
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

        public User ConfirmEmail()
        {
            IsEmailConfirmed = true;
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
