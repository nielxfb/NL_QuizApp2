namespace QuizApp.Application.DTOs.UserSchedule;

public class UserSchedulesDto
{
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public Guid ScheduleId { get; set; }
}