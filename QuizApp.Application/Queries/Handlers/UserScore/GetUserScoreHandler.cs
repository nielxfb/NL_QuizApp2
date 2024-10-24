using QuizApp.Application.DTOs.Quiz;
using QuizApp.Application.DTOs.Schedule;
using QuizApp.Application.DTOs.User;
using QuizApp.Application.DTOs.UserScore;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.UserScore;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.UserScore;

public class GetUserScoreHandler : IQueryHandler<GetUserScoreQuery, UserScoreDto>
{
    private readonly IUserScoreRepository _repository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IUserRepository _userRepository;

    public GetUserScoreHandler(IUserScoreRepository repository, IScheduleRepository scheduleRepository,
        IUserRepository userRepository)
    {
        _repository = repository;
        _scheduleRepository = scheduleRepository;
        _userRepository = userRepository;
    }

    public async Task<UserScoreDto> HandleAsync(GetUserScoreQuery query)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);
        if (user == null)
            throw new ArgumentException("User not found.");

        var schedule = await _scheduleRepository.GetByIdAsync(query.ScheduleId);
        if (schedule == null)
            throw new ArgumentException("Schedule not found.");

        var userSchedule = await _repository.GetAsync(query.UserId, query.ScheduleId);
        if (userSchedule == null)
            throw new ArgumentException("User score not found.");

        return new UserScoreDto
        {
            User = new UserDetailsDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Initial = user.Initial,
            },
            Schedule = new ScheduleDetailsDto
            {
                Id = schedule.ScheduleId,
                Quiz = new QuizDetailsDto
                {
                    Id = schedule.Quiz.QuizId,
                    Title = schedule.Quiz.Title,
                },
                StartDate = schedule.StartDate,
                EndDate = schedule.EndDate,
            },
            Score = userSchedule.Score
        };
    }
}