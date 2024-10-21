using QuizApp.Application.DTOs.Response;

namespace QuizApp.Application.Commands.Response;

public class UpdateResponseCommand
{
    public Guid ResponseId { get; set; }
    public char OptionChoice { get; set; }
    public DateTime AnsweredAt { get; set; }

    public UpdateResponseCommand(UpdateResponseDto dto)
    {
        ResponseId = dto.ResponseId;
        OptionChoice = dto.OptionChoice[0];
        AnsweredAt = DateTime.Now;
    }
}