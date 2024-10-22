using QuizApp.Application.DTOs.Option;

namespace QuizApp.Application.Commands.Option;

public class RemoveOptionCommand
{
    public Guid QuestionId { get; set; }
    public char OptionChoice { get; set; }

    public RemoveOptionCommand(RemoveOptionDto dto)
    {
        QuestionId = dto.QuestionId;
        OptionChoice = dto.OptionChoice[0];
    }
}