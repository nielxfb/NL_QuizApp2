using QuizApp.Application.DTOs.Option;
using QuizApp.Application.DTOs.UserScore;

namespace QuizApp.Application.Commands.UserScore;

public class AddUserScoreCommand
{
    public Guid UserId { get; set; }
    public Guid QuizId { get; set; }
    public int QuestionCount { get; set; }
    public List<OptionDto> SelectedOptions { get; set; }

    public AddUserScoreCommand(AddUserScoreDto dto)
    {
        UserId = dto.UserId;
        QuizId = dto.QuizId;
        QuestionCount = dto.QuestionCount;
        SelectedOptions = dto.SelectedOptions;
    }
}