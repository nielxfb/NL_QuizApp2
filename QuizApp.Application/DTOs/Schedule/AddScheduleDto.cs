namespace QuizApp.Application.DTOs.Schedule;

public class AddScheduleDto
{
    public Guid QuizId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Today;
    public DateTime EndDate { get; set; } = DateTime.Today;
}