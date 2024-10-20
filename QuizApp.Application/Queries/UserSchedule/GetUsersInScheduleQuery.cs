using QuizApp.Application.DTOs.UserSchedule;

namespace QuizApp.Application.Queries.UserSchedule;

public class GetUsersInScheduleQuery
{
    public Guid ScheduleId { get; set; }

    public GetUsersInScheduleQuery(GetUsersInScheduleDto dto)
    {
        ScheduleId = dto.ScheduleId;
    }
}