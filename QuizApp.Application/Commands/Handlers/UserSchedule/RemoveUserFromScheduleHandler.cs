using QuizApp.Application.Commands.UserSchedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.UserSchedule;

public class RemoveUserFromScheduleHandler : ICommandHandler<RemoveUserFromScheduleCommand>
{
    private readonly IUserScheduleRepository _repository;

    public RemoveUserFromScheduleHandler(IUserScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(RemoveUserFromScheduleCommand command)
    {
        var userSchedule = await _repository.GetByScheduleAndUserId(command.UserId, command.ScheduleId);
        if (userSchedule == null) throw new ArgumentException("User not found in schedule.");

        await _repository.RemoveAsync(userSchedule);
    }
}