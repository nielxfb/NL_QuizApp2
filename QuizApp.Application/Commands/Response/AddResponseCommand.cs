using QuizApp.Application.DTOs.Response;

namespace QuizApp.Application.Commands.Response;

public class AddResponseCommand
{
    public Guid ScheduleId { get; set; }
    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
    public char OptionChoice { get; set; }
    public DateTime AnsweredAt { get; set; }

    public AddResponseCommand(AddResponseDto dto)
    {
        ScheduleId = dto.ScheduleId;
        UserId = dto.UserId;
        QuestionId = dto.QuestionId;
        OptionChoice = dto.OptionChoice[0];
        AnsweredAt = DateTime.Now.ToUniversalTime();
    }
}