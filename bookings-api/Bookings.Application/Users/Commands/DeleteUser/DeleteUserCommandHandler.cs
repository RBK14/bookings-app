using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, ErrorOr<Unit>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ErrorOr<Unit>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var userId = UserId.Create(command.UserId);

            if (await _userRepository.GetByIdAsync(userId) is not User user)
                return Errors.User.NotFound;

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
