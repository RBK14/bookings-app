using Bookings.Application.Common.Interfaces.Authentication;
using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.UpdatePassword
{
    public class UpdatePasswordCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : IRequestHandler<UpdatePasswordCommand, ErrorOr<Unit>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<ErrorOr<Unit>> Handle(UpdatePasswordCommand command, CancellationToken cancellationToken)
        {
            var userId = UserId.Create(command.UserId);

            if (await _userRepository.GetByIdAsync(userId) is not User user)
                return Errors.User.NotFound;

            if (userId != user.Id)
                return Errors.User.NoPermissions;

            if (!_passwordHasher.Verify(command.CurrentPassword, user.PasswordHash))
                return Errors.Authentication.InvalidPassword;

            var newPasswordHash = _passwordHasher.HashPassword(command.NewPassword);
            user.UpdatePasswordHash(newPasswordHash);

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
