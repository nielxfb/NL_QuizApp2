using QuizApp.Application.Commands.UserSchedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.UserSchedule;

public class UpdateStatusHandler : ICommandHandler<UpdateStatusCommand>
{
    private readonly IUserScheduleRepository _repository;

    public UpdateStatusHandler(IUserScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateStatusCommand command)
    {
        var userSchedule = await _repository.GetByScheduleAndUserId(command.UserId, command.ScheduleId);
        if (userSchedule == null)
        {
            throw new ArgumentException("User schedule not found.");
        }
        
        userSchedule.Status = command.Status;
        await _repository.UpdateAsync(userSchedule);
    }
}