using QuizApp.Application.Commands.UserSchedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.UserSchedule;

public class AddUserToScheduleHandler : ICommandHandler<AddUserToScheduleCommand>
{
    private readonly IUserScheduleRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IScheduleRepository _scheduleRepository;

    public AddUserToScheduleHandler(IUserScheduleRepository repository, IUserRepository userRepository, IScheduleRepository scheduleRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task HandleAsync(AddUserToScheduleCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null)
            throw new ArgumentException("User not found.");
        
        var schedule = await _scheduleRepository.GetByIdAsync(command.ScheduleId);
        if (schedule == null)
            throw new ArgumentException("Schedule not found.");
        
        if (schedule.UserSchedules.Any(s => s.UserId == command.UserId))
            throw new ArgumentException("User already in schedule.");

        var userSchedule = new Domain.Entities.UserSchedule
        {
            UserId = command.UserId,
            ScheduleId = command.ScheduleId,
            Status = "Incomplete"
        };

        await _repository.AddAsync(userSchedule);
    }
}