namespace QuizApp.Application.DTOs.UserScore;

public class GetScoresByUserDto
{
    public Guid UserId { get; set; } = Guid.Empty;
}