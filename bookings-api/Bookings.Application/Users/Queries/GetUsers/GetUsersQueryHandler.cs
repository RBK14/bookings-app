using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Application.Users.Options;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.Enums;
using MediatR;

namespace Bookings.Application.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<IEnumerable<User>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var role = !string.IsNullOrWhiteSpace(query.Role) && Enum.TryParse<UserRole>(query.Role, true, out var parsedRole)
                ? parsedRole
                : (UserRole?)null;

            var filters = new List<IFilterable<User>>
            {
                new FullNameFilter(query.FullName),
                new RoleFilter(role),
                new IsEmailConfirmedFilter(query.IsEmailConfirmed)
            };

            var sort = new UserSort(query.SortBy);

            return await _userRepository.SearchAsync(filters, sort);
        }
    }
}
