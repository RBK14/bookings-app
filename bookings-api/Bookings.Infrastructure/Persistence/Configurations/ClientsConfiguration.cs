using Bookings.Domain.ClientAggregate;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookings.Infrastructure.Persistence.Configurations
{
    public class ClientsConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            ConfigureClientsTable(builder);
            ConfigureClientAppointments(builder);
        }

        private static void ConfigureClientsTable(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ClientId.Create(value)
                );

            builder.Property(c => c.UserId)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value)
                );

            builder.Property(c => c.FirstName)
                .HasMaxLength(100);

            builder.Property(c => c.LastName)
                .HasMaxLength(100);

            builder.OwnsOne(c => c.Phone, pb =>
            {
                pb.Property(p => p.Value)
                    .HasColumnName("Phone")
                    .HasMaxLength(16);
            });

            builder.Property(c => c.CreatedAt);
            builder.Property(c => c.UpdatedAt);
        }

        private static void ConfigureClientAppointments(EntityTypeBuilder<Client> builder)
        {
            builder.OwnsMany(c => c.AppointmentIds, aib =>
            {
                aib.ToTable("ClientAppointmentIds");

                aib.WithOwner().HasForeignKey("ClientId");

                aib.Property<int>("Id")
                    .UseIdentityColumn(seed: 1000, increment: 1);

                aib.HasKey("Id");

                aib.Property(a => a.Value)
                    .ValueGeneratedNever()
                    .HasColumnName("AppointmentId");
            });

            builder.Metadata.FindNavigation(nameof(Client.AppointmentIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
