namespace QuizApp.Application.DTOs.Schedule;

public class AddScheduleDto
{
    public Guid QuizId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}