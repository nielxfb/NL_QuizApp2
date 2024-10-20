namespace QuizApp.Application.Queries.Schedule;

public class GetScheduleByIdQuery
{
    public Guid Id { get; set; }

    public GetScheduleByIdQuery(Guid id)
    {
        Id = id;
    }
}