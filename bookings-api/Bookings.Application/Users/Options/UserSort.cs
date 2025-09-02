using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.UserAggregate;

namespace Bookings.Application.Users.Options
{
    public class UserSort(string? sortBy) : ISortable<User>
    {
        private readonly string? _sortBy = sortBy;

        public IQueryable<User> Apply(IQueryable<User> query)
        {
            return _sortBy switch
            {
                "FistNameAsc" => query.OrderBy(u => u.FirstName),
                "FirstNameDesc" => query.OrderByDescending(u => u.FirstName),
                "LastNameAsc" => query.OrderBy(u => u.LastName),
                "LastNameDesc" => query.OrderByDescending(u => u.LastName),
                _ => query
            };
        }
    }
}
