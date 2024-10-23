namespace QuizApp.Application.DTOs.Option;

public class OptionDto
{
    public Guid QuestionId { get; set; }
    public char OptionChoice { get; set; } = 'A';
    public string OptionText { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsCorrect { get; set; }
}