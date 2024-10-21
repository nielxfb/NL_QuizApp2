namespace QuizApp.Application.DTOs.Response;

public class GetUserResponsesInQuizDto
{
    public Guid UserId { get; set; }
    public Guid QuizId { get; set; }
}