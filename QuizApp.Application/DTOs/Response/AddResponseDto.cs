namespace QuizApp.Application.DTOs.Response;

public class AddResponseDto
{
    public Guid QuizId { get; set; }
    public Guid UserId { get; set; }
    public Guid QuestionId { get; set; }
    public string OptionChoice { get; set; }
}