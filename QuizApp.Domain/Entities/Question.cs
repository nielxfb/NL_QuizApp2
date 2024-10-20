using System.ComponentModel.DataAnnotations;

namespace QuizApp.Domain.Entities;

public class Question
{
    [Key] [MaxLength(20)] public Guid QuestionId { get; set; } = Guid.Empty;
    [MaxLength(20)] public Guid QuizId { get; set; } = Guid.Empty;
    public Quiz Quiz { get; set; } = null!;
    [MaxLength(50)] public string QuestionText { get; set; } = string.Empty;
    [MaxLength(255)] public string? ImageUrl { get; set; }
    public ICollection<Option> Options { get; } = new List<Option>();
    public Response? Response { get; set; }
}