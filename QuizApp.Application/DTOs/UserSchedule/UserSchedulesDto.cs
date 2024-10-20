namespace QuizApp.Application.DTOs.UserSchedule;

public class UserSchedulesDto
{
    public Guid UserId { get; set; }
    public ICollection<Domain.Entities.Schedule> Schedules { get; set; }
}