namespace QuizApp.Application.DTOs.Schedule;

public class UpdateScheduleDto
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}