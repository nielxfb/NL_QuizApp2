using QuizApp.Application.DTOs.Option;

namespace QuizApp.Application.DTOs.Question;

public class QuestionDto
{
    public Guid QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string? ImageUrl { get; set; }
    public List<OptionDto> Options { get; set; } = new();
}