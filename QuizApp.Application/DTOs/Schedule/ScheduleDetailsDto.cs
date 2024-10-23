using QuizApp.Application.DTOs.Quiz;

namespace QuizApp.Application.DTOs.Schedule;

public class ScheduleDetailsDto
{
    public Guid Id { get; set; }
    public QuizDetailsDto Quiz { get; set; } = new();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}