using FluentValidation;

namespace Bookings.Application.Authentication.Commands.UpdatePassword
{
    public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Identyfikator użytownika jest wymagany.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Obecne hasło jest wymagane.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Nowe hasło jest wymagane.")
                .MinimumLength(8).WithMessage("Hasło musi mieć co najmniej 8 znaków.")
                .Matches("[A-Z]").WithMessage("Hasło musi zawierać co najmniej jedną wielką literę.")
                .Matches("[a-z]").WithMessage("Hasło musi zawierać co najmniej jedną małą literę.")
                .Matches("[0-9]").WithMessage("Hasło musi zawierać co najmniej jedną cyfrę.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Hasło musi zawierać znak specjalny.");

            RuleFor(x => x.ConfirmNewPassword)
                .Matches(x => x.NewPassword).WithMessage("Potwierdzenie hasła musi być identyczne z nowym hasłem.")
                .NotEmpty().WithMessage("Potwierdzenie nowego hasła jest wymagane.");
        }
    }
}
