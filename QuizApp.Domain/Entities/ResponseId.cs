namespace QuizApp.Domain.Entities;

public class ResponseId
{
    public Guid Value { get; }

    public ResponseId(Guid value)
    {
        Value = value;
    }
}