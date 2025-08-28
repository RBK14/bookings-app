using Bookings.Domain.EmployeeAggregate;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookings.Infrastructure.Persistence.Configurations
{
    public class EmployeesConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            ConfigureEmployeesTable(builder);
            ConfigureEmployeeOfferIds(builder);
            ConfigureEmployeeAppointmentIds(builder);
        }

        private static void ConfigureEmployeesTable(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => EmployeeId.Create(value)
                );

            builder.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value)
                );

            builder.Property(e => e.CreatedAt);
            builder.Property(e => e.UpdatedAt);
        }

        private static void ConfigureEmployeeOfferIds(EntityTypeBuilder<Employee> builder)
        {
            builder.OwnsMany(e => e.OfferIds, ob =>
            {
                ob.ToTable("EmployeeOfferIds");

                ob.WithOwner().HasForeignKey("EmployeeId");

                ob.Property<int>("Id")
                    .UseIdentityColumn(seed: 1000, increment: 1);

                ob.HasKey("Id");

                ob.Property(o => o.Value)
                    .ValueGeneratedNever()
                    .HasColumnName("OfferId");
            });

            builder.Metadata.FindNavigation(nameof(Employee.OfferIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private static void ConfigureEmployeeAppointmentIds(EntityTypeBuilder<Employee> builder)
        {
            builder.OwnsMany(e => e.AppointmentIds, ab =>
            {
                ab.ToTable("EmployeeAppointmentIds");

                ab.WithOwner().HasForeignKey("EmployeeId");

                ab.Property<int>("Id")
                    .UseIdentityColumn(seed: 1000, increment: 1);

                ab.HasKey("Id");

                ab.Property(a => a.Value)
                    .ValueGeneratedNever()
                    .HasColumnName("AppointmentId");
            });

            builder.Metadata.FindNavigation(nameof(Employee.AppointmentIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
