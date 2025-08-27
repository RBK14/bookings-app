using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using System.Text.RegularExpressions;

namespace Bookings.Domain.UserAggregate.ValueObjects
{
    public sealed class Email : ValueObject
    {
        private static readonly Regex _regex = new Regex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string Value { get; init; }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Adres email nie może być pusty.");

            //if (!_regex.IsMatch(email))
            //    throw new DomainException("Nieprawidłowy adres email.");

            

            return new Email(email);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private Email()
        {
        }
#pragma warning restore CS8618
    }
}
