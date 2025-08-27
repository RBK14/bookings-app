using Bookings.Domain.Common.Enums;
using Bookings.Domain.Common.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.OfferAggregate;
using Bookings.Domain.OfferAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookings.Infrastructure.Persistence.Configurations
{
    public class OffersConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            ConfigureOffersTable(builder);
            ConfigureOfferAppointmentIds(builder);
        }

        private static void ConfigureOffersTable(EntityTypeBuilder<Offer> builder)
        {
            builder.ToTable("Offers");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => OfferId.Create(value)
                );

            builder.Property(o => o.Name)
                .HasMaxLength(100);

            builder.Property(o => o.Description)
                .HasMaxLength(500);

            builder.Property(o => o.EmployeeId)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => EmployeeId.Create(value)
                );

            builder.OwnsOne(o => o.Price, pb =>
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
                        .HasColumnName("Currency")
                        .HasMaxLength(3)
                        .IsUnicode(false);
                 });

            builder.Property(o => o.Duration)
                .HasConversion(
                    duration => duration.Value,
                    value => Duration.Create(value)
                )
                .HasColumnName("Duration");

            builder.Property(o => o.CreatedAt);
            builder.Property(o => o.UpdatedAt);
        }

        private static void ConfigureOfferAppointmentIds(EntityTypeBuilder<Offer> builder)
        {
            builder.OwnsMany(o => o.AppointmentIds, aib =>
            {
                aib.ToTable("OfferAppointmentIds");

                aib.WithOwner().HasForeignKey("OfferId");

                aib.Property<int>("Id")
                    .UseIdentityColumn(seed: 1000, increment: 1);

                aib.HasKey("Id");

                aib.Property(a => a.Value)
                    .ValueGeneratedNever()
                    .HasColumnName("AppointmentId");
            });

            builder.Metadata.FindNavigation(nameof(Offer.AppointmentIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
