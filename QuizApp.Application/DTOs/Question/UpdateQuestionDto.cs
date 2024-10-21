namespace QuizApp.Application.DTOs.Question;

public class UpdateQuestionDto
{
    public Guid QuestionId { get; set; }
    public Guid QuizId { get; set; }
    public string QuestionText { get; set; }
    public string? ImageUrl { get; set; }
}