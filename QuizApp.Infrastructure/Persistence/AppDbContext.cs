using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Entities;

namespace QuizApp.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Response> Responses { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<UserSchedule> UserSchedules { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.UserSchedules)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        modelBuilder.Entity<Schedule>()
            .HasMany(e => e.UserSchedules)
            .WithOne(e => e.Schedule)
            .HasForeignKey(e => e.ScheduleId)
            .IsRequired();

        modelBuilder.Entity<Quiz>()
            .HasMany(e => e.Schedules)
            .WithOne(e => e.Quiz)
            .HasForeignKey(e => e.QuizId)
            .IsRequired();

        modelBuilder.Entity<Quiz>()
            .HasMany(e => e.Questions)
            .WithOne(e => e.Quiz)
            .HasForeignKey(e => e.QuizId)
            .IsRequired();

        modelBuilder.Entity<Question>()
            .HasMany(e => e.Options)
            .WithOne(e => e.Question)
            .HasForeignKey(e => e.QuestionId)
            .IsRequired();

        modelBuilder.Entity<Quiz>()
            .HasMany(e => e.Responses)
            .WithOne(e => e.Quiz)
            .HasForeignKey(e => e.QuizId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        modelBuilder.Entity<Response>()
            .HasOne(e => e.Question)
            .WithOne(e => e.Response)
            .HasForeignKey<Response>(e => e.QuestionId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(e => e.Responses)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        modelBuilder.Entity<Option>()
            .HasMany(e => e.Responses)
            .WithOne(e => e.Option)
            .HasForeignKey(e => new { e.QuestionId, e.OptionChoice })
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}