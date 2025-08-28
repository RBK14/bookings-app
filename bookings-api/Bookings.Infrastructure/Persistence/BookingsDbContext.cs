using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.OfferAggregate;
using Bookings.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence
{
    public class BookingsDbContext : DbContext
    {

        public BookingsDbContext(
            DbContextOptions<BookingsDbContext> options) : base(options)
        {
        }

        public DbSet<Offer> Offers { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(BookingsDbContext).Assembly);

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                            .SelectMany(t => t.GetProperties())
                            .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
            {
                property.SetPrecision(0);
            }
            
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
