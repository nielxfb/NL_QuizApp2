namespace QuizApp.Domain.Entities;

public class Option
{
    public QuestionId QuestionId { get; set; } = null!;
    public char OptionChoice { get; set; }
    public Question Question { get; set; } = null!;
    public string OptionText { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    
    public ICollection<Response> Responses = new List<Response>();
}