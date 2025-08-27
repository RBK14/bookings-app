using Bookings.Domain.AppointmentAggregate;
using Bookings.Domain.AppointmentAggregate.Enums;
using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.ClientAggregate.ValueObjects;
using Bookings.Domain.Common.Enums;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookings.Infrastructure.Persistence.Configurations
{
    public class AppointmentsConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => AppointmentId.Create(value)
                );

            builder.Property(a => a.OfferId)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => OfferId.Create(value)
                );

            builder.Property(a => a.OfferName)
                .HasColumnName("Name")
                .HasMaxLength(100);

            builder.OwnsOne(a => a.OfferPrice, pb =>
            {
                pb.Property(p => p.Amount)
                    .HasColumnName("Amount")
                    .HasPrecision(18, 2);

                pb.Property(p => p.Currency)
                    .HasColumnName("Currency")
                    .HasConversion(
                        currency => currency.ToString(),
                        value => Enum.Parse<Currency>(value)
                    )
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            builder.Property(a => a.OfferDuration)
                .HasColumnName("Duration")
                .HasConversion(
                    d => d.Value,
                    value => Duration.Create(value)
                );

            builder.Property(a => a.EmployeeId)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => EmployeeId.Create(value)
                );

            builder.Property(a => a.ClientId)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ClientId.Create(value)
                );

            builder.OwnsOne(a => a.Time, tb =>
            {
                tb.Property(t => t.Start)
                    .HasColumnName("StartTime");

                tb.Property(t => t.End)
                    .HasColumnName("EndTime");
            });

            builder.Property(a => a.Status)
                .HasConversion(
                    status => status.ToString(),
                    value => Enum.Parse<AppointmentStatus>(value)
                )
                .HasMaxLength(20);

            builder.Property(a => a.CancelledBy)
               .HasConversion(
                   cancelled => cancelled.ToString(),
                   value => Enum.Parse<CancellationBy>(value)
               )
               .HasMaxLength(20);

            builder.Property(a => a.CreatedAt);
            builder.Property(a => a.UpdatedAt);
        }
    }
}
