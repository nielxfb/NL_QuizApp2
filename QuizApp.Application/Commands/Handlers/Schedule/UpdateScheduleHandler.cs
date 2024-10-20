using QuizApp.Application.Commands.Schedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Schedule;

public class UpdateScheduleHandler : ICommandHandler<UpdateScheduleCommand>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IQuizRepository _quizRepository;

    public UpdateScheduleHandler(IScheduleRepository scheduleRepository, IQuizRepository quizRepository)
    {
        _scheduleRepository = scheduleRepository;
        _quizRepository = quizRepository;
    }

    public async Task HandleAsync(UpdateScheduleCommand command)
    {
        var quiz = await _quizRepository.GetByIdAsync(command.QuizId);
        if (quiz == null)
        {
            throw new ArgumentException("Quiz not found.");
        }
        
        if (command.StartDate < DateTime.UtcNow)
        {
            throw new ArgumentException("Start date must be in the future.");
        }
        
        if (command.StartDate >= command.EndDate)
        {
            throw new ArgumentException("Start date must be before end date.");
        }
        
        var schedule = await _scheduleRepository.GetByIdAsync(command.Id);
        if (schedule == null)
        {
            throw new ArgumentException("Schedule not found.");
        }
        
        schedule.Quiz = quiz;
        schedule.StartDate = command.StartDate;
        schedule.EndDate = command.EndDate;
        
        await _scheduleRepository.UpdateAsync(schedule);
    }
}