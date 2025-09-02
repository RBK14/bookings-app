using FluentValidation;

namespace Bookings.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Identufikator użytownika jest wymagany.");

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

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Numer telefonu jest wymagany.")
                .Matches(@"^\+?[0-9]{7,15}$")
                .WithMessage("Nieprawidłowy numer telefonu.");
        }
    }
}
