namespace QuizApp.Application.DTOs.Question;

public class QuestionDto
{
    public Guid QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string? ImageUrl { get; set; }
}