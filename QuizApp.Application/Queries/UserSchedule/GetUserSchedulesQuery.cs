using QuizApp.Application.DTOs.UserSchedule;

namespace QuizApp.Application.Queries.UserSchedule;

public class GetUserSchedulesQuery
{
    public Guid UserId { get; set; }
    
    public GetUserSchedulesQuery(GetUserSchedulesDto dto)
    {
        UserId = dto.UserId;
    }
}