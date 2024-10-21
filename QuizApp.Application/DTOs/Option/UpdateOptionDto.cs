namespace QuizApp.Application.DTOs.Option;

public class UpdateOptionDto
{
    public Guid QuestionId { get; set; }
    public string OptionChoice { get; set; }
    public string OptionText { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsCorrect { get; set; }
}