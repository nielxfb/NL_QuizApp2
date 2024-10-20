namespace QuizApp.Domain.Entities;

public class QuestionId
{
    public Guid Value { get; }
    
    public QuestionId(Guid value)
    {
        Value = value;
    }
}