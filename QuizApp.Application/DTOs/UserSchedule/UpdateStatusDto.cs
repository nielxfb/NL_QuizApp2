namespace QuizApp.Application.DTOs.UserSchedule;

public class UpdateStatusDto
{
    public Guid ScheduleId { get;set; }
    public Guid UserId { get; set; }
    public string Status { get; set; } = string.Empty;
}