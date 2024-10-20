using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence.Configurations;

public class OptionConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.HasKey(e => new { e.QuestionId, e.OptionChoice });
        builder.Property(e => e.QuestionId)
            .HasConversion(
                id => id.Value,
                value => new QuestionId(value))
            .IsRequired();

        builder.Property(e => e.OptionChoice)
            .IsRequired();

        builder.Property(e => e.OptionText)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(e => e.ImageUrl)
            .HasMaxLength(255);

        builder.Property(e => e.IsCorrect)
            .IsRequired();
    }
}