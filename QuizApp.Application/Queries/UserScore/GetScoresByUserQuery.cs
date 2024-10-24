using QuizApp.Application.DTOs.UserScore;

namespace QuizApp.Application.Queries.UserScore;

public class GetScoresByUserQuery
{
    public Guid UserId { get; set; }

    public GetScoresByUserQuery(GetScoresByUserDto dto)
    {
        UserId = dto.UserId;
    }
}