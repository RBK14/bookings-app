using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Users.Queries.GetUser
{
    public class GetUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, ErrorOr<User>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ErrorOr<User>> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            var userId = UserId.Create(query.UserId);

            if (await _userRepository.GetByIdAsync(userId) is not User user)
                return Errors.User.NotFound;

            return user;
        }
    }
}
