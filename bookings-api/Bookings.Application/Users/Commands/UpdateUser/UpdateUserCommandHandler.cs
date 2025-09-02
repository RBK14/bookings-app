using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, ErrorOr<User>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ErrorOr<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var userId = UserId.Create(command.UserId);

            if (await _userRepository.GetByIdAsync(userId) is not User user)
                return Errors.User.NotFound;

            user.UpdateFirstName(command.FirstName);
            user.UpdateLastName(command.LastName);
            user.UpdateEmail(command.Email);
            user.UpdatePhone(command.Phone);

            await _userRepository.UpdateAsync(user);

            return user;
        }
    }
}
