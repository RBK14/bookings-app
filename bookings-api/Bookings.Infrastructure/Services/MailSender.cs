using Bookings.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Bookings.Infrastructure.Services
{
    public class MailSender(
        IConfiguration configuration)
        : IMailSender
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task SendAsync(string to, string subject, string body)
        {
            var host = _configuration.GetSection("SmtpSettings").GetValue<string>("Host");
            var port = _configuration.GetSection("SmtpSettings").GetValue<int>("Port");
            var from = _configuration.GetSection("SmtpSettings").GetValue<string>("Username");
            var password = _configuration.GetSection("SmtpSettings").GetValue<string>("Password");

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(from, password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
