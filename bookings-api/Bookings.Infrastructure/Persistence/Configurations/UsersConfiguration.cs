using Bookings.Domain.UserAggregate;
using Bookings.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookings.Infrastructure.Persistence.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
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

            builder.Property(u => u.Password)
                .HasMaxLength(255);

            builder.OwnsOne(u => u.Phone, pb =>
            {
                pb.Property(p => p.Value)
                    .HasColumnName("Phone")
                    .HasMaxLength(16);
            });

            builder.Property(u => u.IsEmailConfirmed);

            builder.Property(u => u.ConfirmationCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsRequired(false);

            builder.Property(u => u.CreatedAt);
            builder.Property(u => u.UpdatedAt);
        }
    }
}
