using QuizApp.Application.DTOs.UserSchedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.UserSchedule;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.UserSchedule;

public class GetUserSchedulesHandler : IQueryHandler<GetUserSchedulesQuery, UserSchedulesDto>
{
    private readonly IUserScheduleRepository _repository;
    private readonly IUserRepository _userRepository;

    public GetUserSchedulesHandler(IUserScheduleRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<UserSchedulesDto> HandleAsync(GetUserSchedulesQuery query)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);
        if (user == null) throw new ArgumentException("User not found.");

        var userSchedules = await _repository.GetByUserIdAsync(query.UserId);
        return new UserSchedulesDto
        {
            UserId = query.UserId,
            Schedules = userSchedules
        };
    }
}