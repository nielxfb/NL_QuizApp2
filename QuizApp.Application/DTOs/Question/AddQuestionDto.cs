namespace QuizApp.Application.DTOs.Question;

public class AddQuestionDto
{
    public Guid QuizId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
}