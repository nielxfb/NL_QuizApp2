namespace QuizApp.Application.DTOs.Response;

public class GetUserResponsesInQuizDto
{
    public Guid UserId { get; set; }
    public Guid ScheduleId { get; set; }
}