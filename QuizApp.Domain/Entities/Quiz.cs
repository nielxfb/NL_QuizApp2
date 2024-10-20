namespace QuizApp.Domain.Entities;

public class Quiz
{
    public QuizId Id { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<Response> Responses { get; set; } = new List<Response>();
}