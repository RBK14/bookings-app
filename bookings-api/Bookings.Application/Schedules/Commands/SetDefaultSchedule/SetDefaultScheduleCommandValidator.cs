using FluentValidation;

namespace Bookings.Application.Schedules.Commands.SetDefaultSchedule
{
    public class SetDefaultScheduleCommandValidator : AbstractValidator<SetDefaultScheduleCommand>
    {
        public SetDefaultScheduleCommandValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("Id pracownika jest wymagane.")
                .Must(BeValidGuid).WithMessage("Id pracownika musi być poprawnym identyfikatorem GUID.");

            RuleFor(x => x.Schedules)
                .NotEmpty().WithMessage("Lista domyślnych godzin pracy nie może być pusta.");

            RuleForEach(x => x.Schedules).ChildRules(schedule =>
            {
                schedule.RuleFor(s => s.DayOfWeek)
                    .IsInEnum().WithMessage("Dzień tygodnia jest nieprawidłowy.");

                schedule.RuleFor(s => s.Start)
                    .NotEmpty().WithMessage("Godzina rozpoczęcia jest wymagana.");

                schedule.RuleFor(s => s.End)
                    .NotEmpty().WithMessage("Godzina zakończenia jest wymagana.");

                schedule.RuleFor(s => s)
                    .Must(cmd => cmd.Start < cmd.End)
                    .WithMessage("Godzina rozpoczęcia musi być wcześniejsza niż godzina zakończenia.");
            });
        }

        private static bool BeValidGuid(string employeeId) =>
            Guid.TryParse(employeeId, out _);
    }
}