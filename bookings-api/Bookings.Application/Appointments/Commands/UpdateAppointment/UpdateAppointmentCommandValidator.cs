using FluentValidation;

namespace Bookings.Application.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        public UpdateAppointmentCommandValidator()
        {
            RuleFor(x => x.AppointmentId)
                .NotEmpty().WithMessage("Identyfikator wizyty jest wymagany.");

            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("Identyfikator pracownika jest wymagany.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Data rozpoczęcia jest wymagana.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Data rozpoczęcia musi być w przyszłości.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("Data zakończenia jest wymagana.")
                .GreaterThan(x => x.StartTime).WithMessage("Data zakończenia musi być późniejsza niż data rozpoczęcia.");
        }
            
    }
}
