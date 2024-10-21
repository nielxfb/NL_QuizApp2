using QuizApp.Application.DTOs.Question;

namespace QuizApp.Application.Commands.Question;

public class AddQuestionCommand
{
    public Guid QuizId { get; set; }
    public string QuestionText { get; set; }
    public string? ImageUrl { get; set; }

    public AddQuestionCommand(AddQuestionDto dto)
    {
        QuizId = dto.QuizId;
        QuestionText = dto.QuestionText;
        ImageUrl = dto.ImageUrl;
    }
}