namespace QuizApp.Domain.Entities;

public class UserSchedule
{
    public UserId UserId { get; set; } = null!;
    public ScheduleId ScheduleId { get; set; } = null!;
    public User User { get; set; } = null!;
    public Schedule Schedule { get; set; } = null!;
    public string Status { get; set; } = string.Empty;
}