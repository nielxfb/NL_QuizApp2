using QuizApp.Application.DTOs.Quiz;
using QuizApp.Application.DTOs.Schedule;
using QuizApp.Application.DTOs.User;
using QuizApp.Application.DTOs.UserScore;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.UserScore;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.UserScore;

public class GetScoresByUserHandler : IQueryHandler<GetScoresByUserQuery, List<UserScoreDto>>
{
    private readonly IUserScoreRepository _repository;
    private readonly IUserRepository _userRepository;

    public GetScoresByUserHandler(IUserScoreRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<List<UserScoreDto>> HandleAsync(GetScoresByUserQuery query)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);
        if (user == null)
            throw new ArgumentException("User not found.");
        
        var userScores = await _repository.GetByUserAsync(query.UserId);
        return userScores.Select(userScore => new UserScoreDto
        {
            User = new UserDetailsDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Initial = user.Initial,
            },
            Score = userScore.Score,
            Schedule = new ScheduleDetailsDto
            {
                Id = userScore.Schedule.ScheduleId,
                Quiz = new QuizDetailsDto
                {
                    Id = userScore.Schedule.Quiz.QuizId,
                    Title = userScore.Schedule.Quiz.Title,
                },
                StartDate = userScore.Schedule.StartDate,
                EndDate = userScore.Schedule.EndDate,
            }
        }).ToList();
    }
}