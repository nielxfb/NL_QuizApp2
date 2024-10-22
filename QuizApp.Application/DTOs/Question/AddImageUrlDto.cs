namespace QuizApp.Application.DTOs.Question;

public class AddImageUrlDto
{
    public Guid QuestionId { get; set; }
    public string ImageUrl { get; set; }
}