namespace Bookings.Application.Common.Interfaces.Services
{
    public interface IMailSender
    {
        Task SendAsync(string to, string subject, string body);
    }

}
