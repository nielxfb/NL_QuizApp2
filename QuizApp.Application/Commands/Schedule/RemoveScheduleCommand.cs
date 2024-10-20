namespace QuizApp.Application.Commands.Schedule;

public class RemoveScheduleCommand
{
    public Guid Id { get; set; }

    public RemoveScheduleCommand(Guid id)
    {
        Id = id;
    }
}