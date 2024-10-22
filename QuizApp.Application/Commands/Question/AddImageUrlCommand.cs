using QuizApp.Application.DTOs.Question;

namespace QuizApp.Application.Commands.Question;

public class AddImageUrlCommand
{
    public Guid QuestionId { get; set; }
    public string ImageUrl { get; set; }

    public AddImageUrlCommand(AddImageUrlDto dto)
    {
        QuestionId = dto.QuestionId;
        ImageUrl = dto.ImageUrl;
    }
}