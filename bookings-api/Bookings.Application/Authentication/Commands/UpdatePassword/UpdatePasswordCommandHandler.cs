using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.UpdatePassword
{
    public class UpdatePasswordCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdatePasswordCommand, ErrorOr<Unit>>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ErrorOr<Unit>> Handle(UpdatePasswordCommand command, CancellationToken cancellationToken)
        {
            var userId = UserId.Create(command.UserId);

            if (await _userRepository.GetByIdAsync(userId) is not User user)
                return Errors.User.NotFound;

            if (userId != user.Id)
                return Errors.User.NoPermissions;

            if (!BCrypt.Net.BCrypt.Verify(command.CurrentPassword, user.PasswordHash))
                return Errors.Authentication.InvalidPassword;

            var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(command.NewPassword);
            user.UpdatePasswordHash(newPasswordHash);

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
