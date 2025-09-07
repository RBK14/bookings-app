using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.ClientAggregate;
using Bookings.Domain.Common.Models;
using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.OfferAggregate;
using Bookings.Domain.ScheduleAggregate;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.VerificationTokenAggregate;
using Bookings.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Persistence
{
    public class BookingsDbContext : DbContext
    {
        private readonly PublishDomainEventInterceptor _publishDomainEventInterceptor;
        public BookingsDbContext(
            DbContextOptions<BookingsDbContext> options,
            PublishDomainEventInterceptor publishDomainEventInterceptor) : base(options)
        {
            _publishDomainEventInterceptor = publishDomainEventInterceptor;
        }

        public DbSet<Offer> Offers { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
        public DbSet<VerificationToken> VerificationTokens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Ignore<List<IDomainEvent>>()
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
            optionsBuilder.AddInterceptors(_publishDomainEventInterceptor);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
