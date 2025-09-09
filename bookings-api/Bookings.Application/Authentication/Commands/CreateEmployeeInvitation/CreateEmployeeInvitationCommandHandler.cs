using Bookings.Application.Common.Interfaces.Persistence;
using Bookings.Domain.VerificationTokenAggregate;
using Bookings.Domain.VerificationTokenAggregate.Enums;
using ErrorOr;
using MediatR;

namespace Bookings.Application.Authentication.Commands.CreateEmployeeInvitation
{
    public class CreateEmployeeInvitationCommandHandler(
        IVerificationTokenRepository verificationTokenRepository) : IRequestHandler<CreateEmployeeInvitationCommand, ErrorOr<Unit>>
    {
        private readonly IVerificationTokenRepository _verificationTokenRepository = verificationTokenRepository;

        public async Task<ErrorOr<Unit>> Handle(CreateEmployeeInvitationCommand command, CancellationToken cancellationToken)
        {
            var employeeEmail = command.Email;
            var tokenType = VerificationTokenType.EmailVerification;
            var tokenValidity = TimeSpan.FromMinutes(30);

            var token = VerificationToken.Create(employeeEmail, null, tokenType, tokenValidity);

            _verificationTokenRepository.Add(token);
            await _verificationTokenRepository.SaveChangesAsync();

            // TODO: Send email with registration link

            return Unit.Value;
        }
    }
}
