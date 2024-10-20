using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => new QuestionId(value))
            .IsRequired();

        builder.Property(e => e.QuizId)
            .HasConversion(
                id => id.Value,
                value => new QuizId(value))
            .IsRequired();

        builder.Property(e => e.QuestionText)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.ImageUrl)
            .HasMaxLength(255);

        builder.HasMany(e => e.Options)
            .WithOne(e => e.Question)
            .HasForeignKey(e => e.QuestionId);

        builder.HasOne(e => e.Response)
            .WithOne(e => e.Question)
            .HasForeignKey<Response>(e => e.QuestionId);
    }
}