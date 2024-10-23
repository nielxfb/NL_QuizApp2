using QuizApp.Application.DTOs.Quiz;
using QuizApp.Application.DTOs.Schedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Schedule;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.Schedule;

public class GetScheduleByIdHandler : IQueryHandler<GetScheduleByIdQuery, ScheduleDetailsDto>
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IQuizRepository _quizRepository;

    public GetScheduleByIdHandler(IScheduleRepository scheduleRepository, IQuizRepository quizRepository)
    {
        _scheduleRepository = scheduleRepository;
        _quizRepository = quizRepository;
    }

    public async Task<ScheduleDetailsDto> HandleAsync(GetScheduleByIdQuery query)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(query.Id);
        if (schedule == null) throw new ArgumentException("Schedule not found.");

        var quiz = await _quizRepository.GetByIdAsync(schedule.QuizId);
        if (quiz == null) throw new ArgumentException("Quiz not found.");

        return new ScheduleDetailsDto
        {
            Id = schedule.ScheduleId,
            Quiz = new QuizDetailsDto
            {
                Id = quiz.QuizId,
                Title = quiz.Title,
            },
            StartDate = schedule.StartDate,
            EndDate = schedule.EndDate
        };
    }
}