namespace QuizApp.Domain.Entities;

public class QuizId
{
    public Guid Value { get; }

    public QuizId(Guid value)
    {
        Value = value;
    }
}