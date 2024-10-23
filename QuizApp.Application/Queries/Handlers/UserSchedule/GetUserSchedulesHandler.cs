using QuizApp.Application.DTOs.UserSchedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.UserSchedule;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.UserSchedule;

public class GetUserSchedulesHandler : IQueryHandler<GetUserSchedulesQuery, List<UserSchedulesDto>>
{
    private readonly IUserScheduleRepository _repository;
    private readonly IUserRepository _userRepository;

    public GetUserSchedulesHandler(IUserScheduleRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<List<UserSchedulesDto>> HandleAsync(GetUserSchedulesQuery query)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);
        if (user == null) throw new ArgumentException("User not found.");

        var userSchedules = await _repository.GetByUserIdAsync(query.UserId);
        return userSchedules.Select(us => new UserSchedulesDto
        {
            ScheduleId = us.ScheduleId,
            Title = us.Schedule.Quiz.Title,
            StartDate = us.Schedule.StartDate,
            EndDate = us.Schedule.EndDate,
            Status = us.Status,
        }).ToList();
    }
}