using Bookings.Domain.Common.Enums;
using FluentValidation;

namespace Bookings.Application.Offers.Commands.CreateOffer
{
    public class UpdateOfferCommandValidator : AbstractValidator<CreateOfferCommand>
    {
        public UpdateOfferCommandValidator()
        {
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
                .Must(BeInValidRange).WithMessage("Czas trwania musi być między 15 minut a 8 godzin.");
        }

        private static bool BeValidCurrency(int currency)
        {
            return CurrencyExtensions.IsCorrectCurrencyCode(currency);
        }

        private static bool BeInValidRange(TimeSpan duration)
        {
            return duration.TotalMinutes >= 15 && duration.TotalHours <= 8;
        }
    }
}
