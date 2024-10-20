namespace QuizApp.Domain.Entities;

public class UserId
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        Value = value;
    }
}