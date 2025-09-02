using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.Enums;

namespace Bookings.Application.Users.Options
{
    public class FullNameFilter(string? fullName) : IFilterable<User>
    {
        private readonly string? _fullName = fullName;

        public IQueryable<User> Apply(IQueryable<User> query)
        {
            if (string.IsNullOrWhiteSpace(_fullName))
                return query;

            var parts = _fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
            {
                string namePart = parts[0].ToLower();

                return query.Where(u =>
                    u.FirstName.ToLower().Contains(namePart) ||
                    u.LastName.ToLower().Contains(namePart));
            }

            if (parts.Length >= 2)
            {
                string firstName = parts[0].ToLower();
                string lastName = parts[1].ToLower();

                return query.Where(u =>
                    u.FirstName.ToLower().Contains(firstName) &&
                    u.LastName.ToLower().Contains(lastName));
            }

            return query;
        }
    }

    public class RoleFilter(UserRole? role) : IFilterable<User>
    {
        private readonly UserRole? _role = role;

        public IQueryable<User> Apply(IQueryable<User> query)
        {
            if (_role.HasValue)
                query = query.Where(u => u.Role == _role.Value);

            return query;
        }
    }

    public class IsEmailConfirmedFilter(bool? isEmailAccepted) : IFilterable<User>
    {
        private readonly bool? _isEmailAccepted = isEmailAccepted;

        public IQueryable<User> Apply(IQueryable<User> query)
        {
            if (_isEmailAccepted.HasValue)
                query = query.Where(_ => _isEmailAccepted.Value);

            return query;
        }
    }
}
