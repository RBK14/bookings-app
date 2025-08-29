using Bookings.Domain.Common.Enums;
using FluentValidation;

namespace Bookings.Application.Offers.Commands.UpdateOffer
{
    public class UpdateOfferCommandValidator : AbstractValidator<UpdateOfferCommand>
    {
        public UpdateOfferCommandValidator()
        {
            RuleFor(x => x.OfferId)
                .NotEmpty().WithMessage("Identyfikator oferty jest wymagany.");

            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("Identyfikator pracownika jest wymagany.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa jest wymagana.")
                .MaximumLength(100).WithMessage("Nazwa nie może być dłuższa niż 100 znaków.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Opis jest wymagany.")
                .MaximumLength(500).WithMessage("Opis nie może być dłuższy niż 500 znaków.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Kwota musi być większa od zera.");

            RuleFor(x => x.Currency)
                .Must(BeValidCurrency).WithMessage("Waluta musi być poprawnym kodem ISO 4217 (np. 985, 978, 840).");

            RuleFor(x => x.Duration)
                .NotEmpty().WithMessage("Czas trwania jest wymagany.")
                .Must(BeValidDuration).WithMessage("Czas trwania musi być w formacie hh:mm lub hh:mm:ss.")
                .Must(BeInValidRange).WithMessage("Czas trwania musi być między 15 minut a 8 godzin.");
        }

        private static bool BeValidCurrency(int currency)
        {
            return CurrencyExtensions.IsCorrectCurrencyCode(currency);
        }

        private static bool BeValidDuration(string duration)
        {
            return TimeSpan.TryParse(duration, out _);
        }

        private static bool BeInValidRange(string duration)
        {
            if (!TimeSpan.TryParse(duration, out var parsed))
                return false;

            return parsed.TotalMinutes >= 15 && parsed.TotalHours <= 8;
        }
    }
}
