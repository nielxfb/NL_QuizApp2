namespace QuizApp.Domain.Entities;

public class ScheduleId
{
    public Guid Value { get; }
    
    public ScheduleId(Guid value)
    {
        Value = value;
    }
}