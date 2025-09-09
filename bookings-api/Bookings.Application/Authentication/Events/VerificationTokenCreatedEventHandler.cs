using Bookings.Application.Common.Interfaces.Services;
using Bookings.Domain.VerificationTokenAggregate.Enums;
using Bookings.Domain.VerificationTokenAggregate.Events;
using MediatR;

namespace Bookings.Application.Authentication.Events
{
    public class VerificationTokenCreatedEventHandler(
        IMailSender mailSender)
        : INotificationHandler<VerificationTokenCreatedEvent>
    {
        private readonly IMailSender _mailSender = mailSender;

        public async Task Handle(VerificationTokenCreatedEvent notification, CancellationToken cancellationToken)
        {
            var token = notification.Token;

            var tokenId = token.Id.Value;
            var to = token.Email.Value;
            var tokenType = token.Type;

            string subject;
            string body;

            switch (tokenType)
            {
                case VerificationTokenType.EmailVerification:
                    subject = "Weryfikacja konta klienta";
                    body = $@"
                        <html>
                            <body>
                                <h2>Witaj!</h2>
                                <p>Kliknij link, aby aktywować konto:</p>
                                <p><a href='http://localhost:5173/verify/{tokenId}'>Aktywuj konto</a></p>
                            </body>
                        </html>";
                    break;

                case VerificationTokenType.EmployeeRegistration:
                    subject = "Rejestracja pracownika";
                    body = $@"
                        <html>
                            <body>
                                <h2>Witaj!</h2>
                                <p>Kliknij link, aby zarejestrować konto pracownika:</p>
                                <p><a href='http://localhost:5173/register/{tokenId}'>Zarejestruj konto</a></p>
                            </body>
                        </html>";
                    break;

                case VerificationTokenType.PasswordRecovery:
                    subject = "Przywracanie hasła";
                    body = $@"
                        <html>
                            <body>
                                <h2>Witaj!</h2>
                                <p>Kliknij link, aby zresetować hasło:</p>
                                <p><a href='http://localhost:5173/restore-password/{tokenId}'>Zresetuj hasło</a></p>
                            </body>
                        </html>";
                    break;

                default:
                    throw new InvalidOperationException($"Nieobsługiwany typ tokena: {tokenId}");
            }

            await _mailSender.SendAsync(to, subject, body);
        }
    }
}
