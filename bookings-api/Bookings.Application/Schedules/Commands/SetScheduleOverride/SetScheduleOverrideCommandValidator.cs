using Bookings.Application.Schedules.Commands.SetScheduleOverride;
using FluentValidation;

namespace Bookings.Application.Schedules.Commands.SetDefaultSchedule
{
    public class SetScheduleOverrideCommandValidator : AbstractValidator<SetScheduleOverrideCommand>
    {
        public SetScheduleOverrideCommandValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("Id pracownika jest wymagane.")
                .Must(BeValidGuid).WithMessage("Id pracownika musi być poprawnym identyfikatorem GUID.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Data jest wymagana.");

            RuleFor(x => x.Start)
                .NotEmpty().WithMessage("Godzina rozpoczęcia jest wymagana.");

            RuleFor(x => x.End)
                .NotEmpty().WithMessage("Godzina zakończenia jest wymagana.");

            RuleFor(x => x)
                .Must(cmd => cmd.Start < cmd.End)
                .WithMessage("Godzina rozpoczęcia musi być wcześniejsza niż godzina zakończenia.");
        }

        private static bool BeValidGuid(string employeeId) =>
            Guid.TryParse(employeeId, out _);
    }
}
