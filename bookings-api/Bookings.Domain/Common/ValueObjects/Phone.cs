using Bookings.Domain.Common.Exceptions;
using Bookings.Domain.Common.Models;
using System.Text.RegularExpressions;

namespace Bookings.Domain.Common.ValueObjects
{
    public sealed class Phone : ValueObject
    {
        private static readonly Regex _regex = new Regex(
            @"^\+?[1-9]\d{7,14}$",
            RegexOptions.Compiled);

        public string Value { get; init; }

        private Phone(string value)
        {
            Value = value;
        }

        public static Phone Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainException("Numer telefonu nie może być pusty.");

            //if (!_regex.IsMatch(phone))
            //    throw new DomainException("Nieprawidłowy numer telefonu.");

            return new Phone(phone);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

#pragma warning disable CS8618
        private Phone()
        {
        }
#pragma warning restore CS8618
    }
}
