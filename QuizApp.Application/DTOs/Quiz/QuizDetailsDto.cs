using QuizApp.Application.DTOs.Question;

namespace QuizApp.Application.DTOs.Quiz;

public class QuizDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<QuestionDto> Questions = new();
}