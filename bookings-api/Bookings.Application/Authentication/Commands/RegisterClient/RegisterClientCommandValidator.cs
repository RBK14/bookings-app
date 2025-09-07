using FluentValidation;

namespace Bookings.Application.Authentication.Commands.RegisterClient
{
    public class RegisterClientCommandValidator : AbstractValidator<RegisterClientCommand>
    {
        public RegisterClientCommandValidator()
        {
            RuleFor(x => x.FirstName)
                 .NotEmpty().WithMessage("Imię jest wymagane.")
                 .MaximumLength(100).WithMessage("Imię nie może być dłuższe niż 100 znaków.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Nazwisko jest wymagane.")
                .MaximumLength(100).WithMessage("Nazwisko nie może być dłuższe niż 100 znaków.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email jest wymagany.")
                .EmailAddress().WithMessage("Nieprawidłowy adres email.")
                .MaximumLength(255);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Hasło jest wymagane.")
                .MinimumLength(8).WithMessage("Hasło musi mieć co najmniej 8 znaków.")
                .Matches("[A-Z]").WithMessage("Hasło musi zawierać co najmniej jedną wielką literę.")
                .Matches("[a-z]").WithMessage("Hasło musi zawierać co najmniej jedną małą literę.")
                .Matches("[0-9]").WithMessage("Hasło musi zawierać co najmniej jedną cyfrę.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Hasło musi zawierać znak specjalny.");

            RuleFor(x => x.ConfirmPassword)
                .Matches(x => x.Password).WithMessage("Potwierdzenie hasła musi być identyczne z hasłem.")
                .NotEmpty().WithMessage("Potwierdzenie nowego hasła jest wymagane.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Numer telefonu jest wymagany.")
                .Matches(@"^\+?[0-9]{7,15}$")
                .WithMessage("Nieprawidłowy numer telefonu.");
        }
    }
}
