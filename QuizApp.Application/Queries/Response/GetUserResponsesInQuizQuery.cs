using QuizApp.Application.DTOs.Response;

namespace QuizApp.Application.Queries.Response;

public class GetUserResponsesInQuizQuery
{
    public Guid UserId { get; set; }
    public Guid ScheduleId { get; set; }

    public GetUserResponsesInQuizQuery(GetUserResponsesInQuizDto dto)
    {
        UserId = dto.UserId;
        ScheduleId = dto.ScheduleId;
    }
}