namespace QuizApp.Domain.Entities;

public class UserScheduleId
{
    public UserId UserId { get; }
    public ScheduleId ScheduleId { get; }
    
    public UserScheduleId(UserId userId, ScheduleId scheduleId)
    {
        UserId = userId;
        ScheduleId = scheduleId;
    }
}