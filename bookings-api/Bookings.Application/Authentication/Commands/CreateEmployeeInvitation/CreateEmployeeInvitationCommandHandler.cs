using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.Common.Errors;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.VerificationTokenAggregate;
using Bookings.Domain.VerificationTokenAggregate.Enums;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.CreateEmployeeInvitation
{
    public class CreateEmployeeInvitationCommandHandler(
        IVerificationTokenRepository verificationTokenRepository,
        IUserRepository userRepository)
        : IRequestHandler<CreateEmployeeInvitationCommand, ErrorOr<Unit>>
    {
        private readonly IVerificationTokenRepository _verificationTokenRepository = verificationTokenRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<ErrorOr<Unit>> Handle(CreateEmployeeInvitationCommand command, CancellationToken cancellationToken)
        {
            var employeeEmail = command.Email;
            var tokenType = VerificationTokenType.EmployeeRegistration;
            var tokenValidity = TimeSpan.FromMinutes(30);

            if (await _userRepository.GetByEmailAsync(Email.Create(employeeEmail)) is not null)
                return Errors.User.DuplicateEmail;

            var token = VerificationToken.Create(employeeEmail, null, tokenType, tokenValidity);

            _verificationTokenRepository.Add(token);
            await _verificationTokenRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
