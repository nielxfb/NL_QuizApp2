using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => new UserId(value))
            .IsRequired();
        
        builder.Property(e => e.FullName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.Initial)
            .IsRequired()
            .HasMaxLength(10);
        
        builder.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.Role)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasMany(e => e.Responses)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        builder.HasMany(e => e.UserSchedules)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}