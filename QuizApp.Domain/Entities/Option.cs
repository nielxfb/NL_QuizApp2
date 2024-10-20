using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace QuizApp.Domain.Entities;

[PrimaryKey(nameof(QuestionId), nameof(OptionChoice))]
public class Option
{
    [MaxLength(20)] public Guid QuestionId { get; set; } = Guid.Empty;
    public Question Question { get; set; } = null!;
    public char OptionChoice { get; set; }
    [MaxLength(20)] public string OptionText { get; set; } = string.Empty;
    [MaxLength(255)] public string? ImageUrl { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public ICollection<Response> Responses { get; } = new List<Response>();
}