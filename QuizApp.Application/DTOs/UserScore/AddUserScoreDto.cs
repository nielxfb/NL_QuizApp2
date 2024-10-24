using QuizApp.Application.DTOs.Option;

namespace QuizApp.Application.DTOs.UserScore;

public class AddUserScoreDto
{
    public Guid UserId { get; set; } = Guid.Empty;
    public Guid ScheduleId { get; set; } = Guid.Empty;
    public int QuestionCount { get; set; }
    public List<OptionDto> SelectedOptions { get; set; } = new();
}