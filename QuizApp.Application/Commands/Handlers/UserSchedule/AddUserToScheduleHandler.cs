using QuizApp.Application.Commands.UserSchedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.UserSchedule;

public class AddUserToScheduleHandler : ICommandHandler<AddUserToScheduleCommand>
{
    private readonly IUserScheduleRepository _repository;

    public AddUserToScheduleHandler(IUserScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddUserToScheduleCommand command)
    {
        var userSchedule = new Domain.Entities.UserSchedule
        {
            UserId = command.UserId,
            ScheduleId = command.ScheduleId,
            Status = "Incomplete"
        };
        
        await _repository.AddAsync(userSchedule);
    }
}