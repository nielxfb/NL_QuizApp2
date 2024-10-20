using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence.Configurations;

public class ResponseConfiguration : IEntityTypeConfiguration<Response>
{
    public void Configure(EntityTypeBuilder<Response> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => new ResponseId(value))
            .IsRequired();

        builder.Property(e => e.QuizId)
            .HasConversion(
                id => id.Value,
                value => new QuizId(value))
            .IsRequired();

        builder.Property(e => e.UserId)
            .HasConversion(
                id => id.Value,
                value => new UserId(value))
            .IsRequired();

        builder.Property(e => e.QuestionId)
            .HasConversion(
                id => id.Value,
                value => new QuestionId(value))
            .IsRequired();

        builder.Property(e => e.OptionChoice)
            .IsRequired();

        builder.HasOne(e => e.Option)
            .WithMany(e => e.Responses)
            .HasForeignKey(e => new { e.QuestionId, e.OptionChoice })
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}