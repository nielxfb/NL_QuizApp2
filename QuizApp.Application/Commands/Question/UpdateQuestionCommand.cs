using QuizApp.Application.DTOs.Question;

namespace QuizApp.Application.Commands.Question;

public class UpdateQuestionCommand
{
    public Guid QuestionId { get; set; }
    public Guid QuizId { get; set; }
    public string QuestionText { get; set; }
    public string? ImageUrl { get; set; }

    public UpdateQuestionCommand(UpdateQuestionDto dto)
    {
        QuestionId = dto.QuestionId;
        QuizId = dto.QuizId;
        QuestionText = dto.QuestionText;
        ImageUrl = dto.ImageUrl;
    }
}