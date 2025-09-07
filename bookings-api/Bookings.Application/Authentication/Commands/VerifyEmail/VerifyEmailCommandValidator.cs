using FluentValidation;

namespace Bookings.Application.Authentication.Commands.VerifyEmail
{
    public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(x => x.TokenId)
               .NotEmpty().WithMessage("Token jest wymagany.");
        }
    }
}
