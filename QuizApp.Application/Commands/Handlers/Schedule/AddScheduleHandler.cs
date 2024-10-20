using QuizApp.Application.Commands.Schedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Schedule;

public class AddScheduleHandler : ICommandHandler<AddScheduleCommand>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IQuizRepository _quizRepository;

    public AddScheduleHandler(IScheduleRepository scheduleRepository, IQuizRepository quizRepository)
    {
        _scheduleRepository = scheduleRepository;
        _quizRepository = quizRepository;
    }

    public async Task HandleAsync(AddScheduleCommand command)
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

        var schedule = new Domain.Entities.Schedule
        {
            ScheduleId = Guid.NewGuid(),
            Quiz = quiz,
            StartDate = command.StartDate,
            EndDate = command.EndDate
        };
            
        await _scheduleRepository.AddAsync(schedule);
    }
}