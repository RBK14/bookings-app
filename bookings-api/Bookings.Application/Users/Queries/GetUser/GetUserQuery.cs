using Bookings.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Users.Queries.GetUser
{
    public record GetUserQuery(string UserId) : IRequest<ErrorOr<User>>;
}
