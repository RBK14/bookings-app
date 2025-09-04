using Bookings.Domain.AppointmentAggregate.ValueObjects;
using Bookings.Domain.EmployeeAggregate.ValueObjects;
using Bookings.Domain.ScheduleAggregate;
using Bookings.Domain.ScheduleAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace Bookings.Infrastructure.Persistence.Configurations
{
    public class SchedulesConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            ConfigureSchedulesTable(builder);
            ConfigureDefaultSchedulesTable(builder);
            ConfigureOverrides(builder);
            ConfigureSlots(builder);
        }

        private void ConfigureSchedulesTable(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedules");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ScheduleId.Create(value)
                );

            builder.Property(s => s.EmployeeId)
                .HasConversion(
                    id => id.Value,
                    value => EmployeeId.Create(value)
                );

            builder.Property(s => s.CreatedAt);
            builder.Property(s => s.UpdatedAt);
        }

        private void ConfigureDefaultSchedulesTable(EntityTypeBuilder<Schedule> builder)
        {
            builder.OwnsMany(s => s.DefaultSchedules, dsb =>
            {
                dsb.ToTable("DefaultSchedules");

                dsb.WithOwner().HasForeignKey("ScheduleId");

                dsb.HasKey("Id", "ScheduleId");

                dsb.Property(ds => ds.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("DefaultScheduleId")
                    .HasConversion(
                        id => id.Value,
                        value => WorkDayScheduleId.Create(value)
                    );

                dsb.Property(d => d.DayOfWeek);

                dsb.Property(d => d.Start)
                    .HasConversion(
                        t => t.ToTimeSpan(),
                        v => TimeOnly.FromTimeSpan(v)
                    );

                dsb.Property(d => d.End)
                    .HasConversion(
                        t => t.ToTimeSpan(),
                        v => TimeOnly.FromTimeSpan(v)
                    );
            });

            builder.Metadata.FindNavigation(nameof(Schedule.DefaultSchedules))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private static void ConfigureOverrides(EntityTypeBuilder<Schedule> builder)
        {
            builder.OwnsMany(s => s.Overrides, ob =>
            {
                ob.ToTable("Overrides");

                ob.WithOwner().HasForeignKey("ScheduleId");

                ob.HasKey("Id", "ScheduleId");

                ob.Property(ds => ds.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("OverrideId")
                    .HasConversion(
                        id => id.Value,
                        value => WorkDayOverrideId.Create(value)
                    );

                ob.Property(o => o.Date)
                    .HasConversion(
                        d => d.ToDateTime(TimeOnly.MinValue),
                        dt => DateOnly.FromDateTime(dt)
                    );

                ob.Property(o => o.Start)
                    .HasConversion(
                        t => t.ToTimeSpan(),
                        v => TimeOnly.FromTimeSpan(v)
                    );

                ob.Property(o => o.End)
                    .HasConversion(
                        t => t.ToTimeSpan(),
                        v => TimeOnly.FromTimeSpan(v)
                    );
            });

            builder.Metadata.FindNavigation(nameof(Schedule.Overrides))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private static void ConfigureSlots(EntityTypeBuilder<Schedule> builder)
        {
            builder.OwnsMany(s => s.Slots, sb =>
            {
                sb.ToTable("Slots");

                sb.WithOwner().HasForeignKey("ScheduleId");

                sb.HasKey("Id", "ScheduleId");

                sb.Property(s => s.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("SlotId")
                    .HasConversion(
                        id => id.Value,
                        value => WorkSlotId.Create(value)
                    );

                sb.Property(s => s.AppointmentId)
                    .HasConversion(
                        id => id.Value,
                        value => AppointmentId.Create(value)
                    );

                sb.Property(s => s.Start)
                    .HasPrecision(0);

                sb.Property(s => s.End)
                    .HasPrecision(0);
            });

            builder.Metadata.FindNavigation(nameof(Schedule.Slots))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
