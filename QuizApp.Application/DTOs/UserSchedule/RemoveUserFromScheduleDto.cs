namespace QuizApp.Application.DTOs.UserSchedule;

public class RemoveUserFromScheduleDto
{
    public Guid UserId { get; set; }
    public Guid ScheduleId { get; set; }
}