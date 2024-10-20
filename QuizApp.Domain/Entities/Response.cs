namespace QuizApp.Domain.Entities;

public class Response
{
    public ResponseId Id { get; set; } = null!;
    public QuizId QuizId { get; set; } = null!;
    public Quiz Quiz { get; set; } = null!;
    public UserId UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    public QuestionId QuestionId { get; set; } = null!;
    public Question Question { get; set; } = null!;
    public char OptionChoice { get; set; }
    public Option Option { get; set; } = null!;
    public bool IsCorrect { get; set; }
    public DateTime AnsweredAt { get; set; }
}