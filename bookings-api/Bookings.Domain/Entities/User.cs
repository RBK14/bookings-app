using Bookings.Domain.Enums;

namespace Bookings.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserRole Role { get; set; }

        public User(string name, string phone, string email, string password, UserRole role = UserRole.Customer)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
