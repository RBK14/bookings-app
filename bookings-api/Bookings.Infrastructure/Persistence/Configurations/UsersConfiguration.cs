using Bookings.Domain.Common.Enums;
using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.Enums;
using Bookings.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookings.Infrastructure.Persistence.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            ConfigureUsersTable(builder);
            ConfigureUserVerificationTokens(builder);
        }

        private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value)
                );

            builder.Property(u => u.FirstName)
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .HasMaxLength(50);

            builder.OwnsOne(u => u.Email, eb =>
            {
                eb.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(255);
            });

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(255);

            builder.OwnsOne(u => u.Phone, pb =>
            {
                pb.Property(p => p.Value)
                    .HasColumnName("Phone")
                    .HasMaxLength(16);
            });

            builder.Property(u => u.Role)
                .HasConversion(
                    currency => currency.ToString(),
                    value => Enum.Parse<UserRole>(value))
                .IsUnicode(false);

            builder.Property(u => u.IsEmailVerified);

            builder.Property(u => u.CreatedAt);
            builder.Property(u => u.UpdatedAt);
        }

        private static void ConfigureUserVerificationTokens(EntityTypeBuilder<User> builder)
        {
            builder.OwnsMany(u => u.VerificationTokenIds, vtb =>
            {
                vtb.ToTable("UserVerificationTokenIds");

                vtb.WithOwner().HasForeignKey("UserId");

                vtb.Property<int>("Id")
                    .UseIdentityColumn(seed: 1000, increment: 1);

                vtb.HasKey("Id");

                vtb.Property(v => v.Value)
                    .ValueGeneratedNever()
                    .HasColumnName("VerificationTokenId");
            });

            builder.Metadata.FindNavigation(nameof(User.VerificationTokenIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
