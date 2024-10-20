using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence.Configurations;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id)
            .HasConversion(
                id => id.Value,
                value => new QuizId(value))
            .IsRequired();

        builder.Property(q => q.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(e => e.Schedules)
            .WithOne(e => e.Quiz)
            .HasForeignKey(e => e.QuizId)
            .IsRequired();

        builder.HasMany(e => e.Questions)
            .WithOne(e => e.Quiz)
            .HasForeignKey(e => e.QuizId)
            .IsRequired();

        builder.HasMany(e => e.Responses)
            .WithOne(e => e.Quiz)
            .HasForeignKey(e => e.QuizId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}