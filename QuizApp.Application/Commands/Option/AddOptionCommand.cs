using QuizApp.Application.DTOs.Option;

namespace QuizApp.Application.Commands.Option;

public class AddOptionCommand
{
    public Guid QuestionId { get; set; }
    public char OptionChoice { get; set; }
    public string OptionText { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsCorrect { get; set; }

    public AddOptionCommand(AddOptionDto dto)
    {
        QuestionId = dto.QuestionId;
        OptionChoice = dto.OptionChoice[0];
        OptionText = dto.OptionText;
        ImageUrl = dto.ImageUrl;
        IsCorrect = dto.IsCorrect;
    }
}