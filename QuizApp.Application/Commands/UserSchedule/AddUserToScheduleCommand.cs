using QuizApp.Application.DTOs.UserSchedule;

namespace QuizApp.Application.Commands.UserSchedule;

public class AddUserToScheduleCommand
{
    public Guid UserId { get; set; }
    public Guid ScheduleId { get; set; }

    public AddUserToScheduleCommand(AddUserToScheduleDto dto)
    {
        UserId = dto.UserId;
        ScheduleId = dto.ScheduleId;
    }
}