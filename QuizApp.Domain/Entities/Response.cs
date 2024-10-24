using System.ComponentModel.DataAnnotations;

namespace QuizApp.Domain.Entities;

public class Response
{
    [Key] [MaxLength(20)] public Guid ResponseId { get; set; } = Guid.Empty;
    [MaxLength(20)] public Guid ScheduleId { get; set; } = Guid.Empty;
    public Schedule Schedule { get; set; } = null!;
    [MaxLength(20)] public Guid UserId { get; set; } = Guid.Empty;
    public User User { get; set; } = null!;
    [MaxLength(20)] public Guid QuestionId { get; set; } = Guid.Empty;
    public Question Question { get; set; } = null!;
    public char OptionChoice { get; set; }
    public Option Option { get; set; } = null!;
    public bool IsCorrect { get; set; }
    public DateTime AnsweredAt { get; set; }
}