using Bookings.Domain.UserAggregate.ValueObjects;
using Bookings.Domain.VerificationTokenAggregate;
using Bookings.Domain.VerificationTokenAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookings.Infrastructure.Persistence.Configurations
{
    public class VerificationTokensConfiguration : IEntityTypeConfiguration<VerificationToken>
    {
        public void Configure(EntityTypeBuilder<VerificationToken> builder)
        {
            builder.ToTable("VerificationTokens");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => VerificationTokenId.Create(value)
                );

            builder.OwnsOne(u => u.Email, eb =>
            {
                eb.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(255);
            });

            builder.Property(t => t.UserId)
                .HasConversion(
                    id => id! != null! ? id.Value : (Guid?)null,
                    value => value.HasValue ? UserId.Create(value.Value) : null
                );

            builder.Property(t => t.ExpiresAt);

            builder.Property(t => t.UsedAt);

            builder.Ignore(t => t.IsUsed);
        }
    }
}
