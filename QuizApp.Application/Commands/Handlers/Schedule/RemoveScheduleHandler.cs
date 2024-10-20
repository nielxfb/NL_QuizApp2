using QuizApp.Application.Commands.Schedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Schedule;

public class RemoveScheduleHandler : ICommandHandler<RemoveScheduleCommand>
{
    private readonly IScheduleRepository _repository;

    public RemoveScheduleHandler(IScheduleRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(RemoveScheduleCommand command)
    {
        var schedule = await _repository.GetByIdAsync(new ScheduleId(command.Id));
        if (schedule == null)
        {
            throw new ArgumentException("Schedule not found.");
        }
        
        await _repository.RemoveAsync(schedule);
    }
}