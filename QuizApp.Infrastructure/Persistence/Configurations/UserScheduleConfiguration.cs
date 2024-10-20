using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence.Configurations;

public class UserScheduleConfiguration : IEntityTypeConfiguration<UserSchedule>
{
    public void Configure(EntityTypeBuilder<UserSchedule> builder)
    {
        builder.HasKey(e => new { e.UserId, e.ScheduleId });
        builder.Property(e => e.UserId)
            .HasConversion(
                id => id.Value,
                value => new UserId(value))
            .IsRequired();

        builder.Property(e => e.ScheduleId)
            .HasConversion(
                id => id.Value,
                value => new ScheduleId(value))
            .IsRequired();

        builder.Property(e => e.Status)
            .IsRequired()
            .HasMaxLength(10);
    }
}