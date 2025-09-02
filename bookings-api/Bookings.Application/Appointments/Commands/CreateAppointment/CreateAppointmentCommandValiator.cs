using FluentValidation;

namespace Bookings.Application.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(x => x.OfferId)
                .NotEmpty().WithMessage("Id oferty jest wymagane.");

            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("Id klienta jest wymagane.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Data i godzina rozpoczęcia są wymagane.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Data rozpoczęcia musi być w przyszłości.");
        }
    }
}
