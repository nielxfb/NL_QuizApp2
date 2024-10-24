using QuizApp.Application.DTOs.UserScore;

namespace QuizApp.Application.Queries.UserScore;

public class GetUserScoreQuery
{
    public Guid UserId { get; set; }
    public Guid ScheduleId { get; set; }

    public GetUserScoreQuery(GetUserScoreDto dto)
    {
        UserId = dto.UserId;
        ScheduleId = dto.ScheduleId;
    }
}