namespace QuizApp.Domain.Entities;

public class Schedule
{
    public ScheduleId Id { get; set; } = null!;
    public QuizId QuizId { get; set; } = null!;
    public Quiz Quiz { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<UserSchedule> UserSchedules = new List<UserSchedule>();
}