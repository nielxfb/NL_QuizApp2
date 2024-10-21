using QuizApp.Application.DTOs.UserSchedule;

namespace QuizApp.Application.Commands.UserSchedule;

public class RemoveUserFromScheduleCommand
{
    public Guid UserId { get; set; }
    public Guid ScheduleId { get; set; }

    public RemoveUserFromScheduleCommand(RemoveUserFromScheduleDto dto)
    {
        UserId = dto.UserId;
        ScheduleId = dto.ScheduleId;
    }
}