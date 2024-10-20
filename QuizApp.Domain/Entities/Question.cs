namespace QuizApp.Domain.Entities;

public class Question
{
    public QuestionId Id { get; set; } = null!;
    public QuizId QuizId { get; set; } = null!;
    public Quiz Quiz { get; set; } = null!;
    public string QuestionText { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public ICollection<Option> Options = new List<Option>();
    public Response? Response { get; set; }
}