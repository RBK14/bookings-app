using Bookings.Domain.UserAggregate;
using MediatR;

namespace Bookings.Application.Users.Queries.GetUsers
{
    public record GetUsersQuery(
        string? FullName,
        string? Role,
        bool? IsEmailConfirmed,
        string? SortBy) : IRequest<IEnumerable<User>>;
}
