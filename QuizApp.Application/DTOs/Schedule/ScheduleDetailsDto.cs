namespace QuizApp.Application.DTOs.Schedule;

public class ScheduleDetailsDto
{
    public Guid Id { get; set; }
    public Domain.Entities.Quiz Quiz { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}