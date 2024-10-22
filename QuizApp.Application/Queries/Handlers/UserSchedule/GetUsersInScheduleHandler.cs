using QuizApp.Application.DTOs.User;
using QuizApp.Application.DTOs.UserSchedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.UserSchedule;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.UserSchedule;

public class GetUsersInScheduleHandler : IQueryHandler<GetUsersInScheduleQuery, UsersInScheduleDto>
{
    private readonly IUserScheduleRepository _repository;
    private readonly IScheduleRepository _scheduleRepository;

    public GetUsersInScheduleHandler(IUserScheduleRepository repository, IScheduleRepository scheduleRepository)
    {
        _repository = repository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task<UsersInScheduleDto> HandleAsync(GetUsersInScheduleQuery query)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(query.ScheduleId);
        if (schedule == null) throw new ArgumentException("Schedule not found.");

        var users = await _repository.GetByScheduleIdAsync(query.ScheduleId);
        if (users == null) throw new ArgumentException("No users found in this schedule.");

        return new UsersInScheduleDto
        {
            ScheduleId = query.ScheduleId,
            Users = users.Select(u => new UserDto
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Initial = u.Initial
            }).ToList()
        };
    }
}