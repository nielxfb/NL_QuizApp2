using QuizApp.Application.DTOs.UserSchedule;

namespace QuizApp.Application.Commands.UserSchedule;

public class UpdateStatusCommand
{
    public Guid ScheduleId { get;set; }
    public Guid UserId { get; set; }
    public string Status { get; set; } = string.Empty;

    public UpdateStatusCommand(UpdateStatusDto dto)
    {
        ScheduleId = dto.ScheduleId;
        UserId = dto.UserId;
        Status = dto.Status;
    }
}