namespace Bookings.Application.Common.Interfaces.Authentication
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool Verify(string password, string passwordHash);
    }
}
