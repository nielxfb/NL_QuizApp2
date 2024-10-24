namespace QuizApp.Application.DTOs.UserScore;

public class GetUserScoreDto
{
    public Guid UserId { get; set; } = Guid.Empty;
    public Guid ScheduleId { get; set; } = Guid.Empty;
}