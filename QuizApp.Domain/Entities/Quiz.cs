using System.ComponentModel.DataAnnotations;

namespace QuizApp.Domain.Entities;

public class Quiz
{
    [Key] [MaxLength(20)] public Guid QuizId { get; set; } = Guid.Empty;
    [MaxLength(20)] public string Title { get; set; } = string.Empty;
    public ICollection<Schedule> Schedules { get; } = new List<Schedule>();
    public ICollection<Question> Questions { get; } = new List<Question>();
    public ICollection<Response> Responses { get; } = new List<Response>();
}