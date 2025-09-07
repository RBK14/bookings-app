using FluentValidation;

namespace Bookings.Application.Authentication.Commands.CreateEmployeeInvitation
{
    public class CreateEmployeeInvitationCommandValidator : AbstractValidator<CreateEmployeeInvitationCommand>
    {
        public CreateEmployeeInvitationCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email jest wymagany.")
                .EmailAddress().WithMessage("Nieprawidłowy adres email.")
                .MaximumLength(255);
        }
    }
}
