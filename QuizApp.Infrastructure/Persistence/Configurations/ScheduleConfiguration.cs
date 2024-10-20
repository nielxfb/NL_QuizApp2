using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => new ScheduleId(value))
            .IsRequired();

        builder.Property(s => s.QuizId)
            .HasConversion(
                id => id.Value,
                value => new QuizId(value))
            .IsRequired();

        builder.Property(s => s.StartDate)
            .IsRequired();

        builder.Property(s => s.EndDate)
            .IsRequired();

        builder.HasMany(e => e.UserSchedules)
            .WithOne(e => e.Schedule)
            .HasForeignKey(e => e.ScheduleId)
            .IsRequired();
    }
}