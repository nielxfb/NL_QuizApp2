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
    public DbSet<UserScore> UserScores { get; set; }

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
            .HasMany(e => e.Schedules)
            .WithOne(e => e.Quiz)
            .HasForeignKey(e => e.QuizId)
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

        modelBuilder.Entity<Response>()
            .HasOne(e => e.Schedule)
            .WithMany(e => e.Responses)
            .HasForeignKey(e => e.ScheduleId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        modelBuilder.Entity<Option>()
            .HasMany(e => e.Responses)
            .WithOne(e => e.Option)
            .HasForeignKey(e => new { e.QuestionId, e.OptionChoice })
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        modelBuilder.Entity<UserScore>()
            .HasOne(e => e.User)
            .WithMany(e => e.Scores)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        modelBuilder.Entity<UserScore>()
            .HasOne(e => e.Schedule)
            .WithMany(e => e.UserScores)
            .HasForeignKey(e => e.ScheduleId)
            .IsRequired();

        var userId = Guid.NewGuid();
        modelBuilder.Entity<User>()
            .HasData(
                new User
                {
                    UserId = Guid.NewGuid(),
                    FullName = "Admin",
                    Role = "Admin",
                    Initial = "admin",
                    Password = "dummypassword",
                },
                new User
                {
                    UserId = userId,
                    FullName = "Daniel Adamlu",
                    Role = "User",
                    Initial = "NL23-2",
                    Password = "dummypassword",
                });


        var quizId = Guid.NewGuid();
        modelBuilder.Entity<Quiz>()
            .HasData(
                new Quiz
                {
                    QuizId = quizId,
                    Title = "Mock Quiz",
                }
            );

        var questionId = Guid.NewGuid();
        modelBuilder.Entity<Question>()
            .HasData(
                new Question
                {
                    QuizId = quizId,
                    QuestionId = questionId,
                    QuestionText = "What is the capital of Indonesia?",
                }
            );

        modelBuilder.Entity<Option>()
            .HasData(
                new Option
                {
                    QuestionId = questionId,
                    OptionChoice = 'A',
                    OptionText = "Medan",
                    IsCorrect = false,
                },
                new Option
                {
                    QuestionId = questionId,
                    OptionChoice = 'B',
                    OptionText = "Bandung",
                    IsCorrect = false,
                },
                new Option
                {
                    QuestionId = questionId,
                    OptionChoice = 'C',
                    OptionText = "Jakarta",
                    IsCorrect = true,
                }
            );

        var scheduleId = Guid.NewGuid();
        modelBuilder.Entity<Schedule>()
            .HasData(
                new Schedule
                {
                    ScheduleId = scheduleId,
                    QuizId = quizId,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(1),
                }
            );

        modelBuilder.Entity<UserSchedule>()
            .HasData(
                new UserSchedule
                {
                    UserId = userId,
                    ScheduleId = scheduleId,
                    Status = "Incomplete",
                }
            );

        base.OnModelCreating(modelBuilder);
    }
}