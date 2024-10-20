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
        var schedule = await _scheduleRepository.GetByIdAsync(new ScheduleId(query.Id));
        if (schedule == null)
        {
            throw new ArgumentException("Schedule not found.");
        }

        var quiz = await _quizRepository.GetByIdAsync(new QuizId(schedule.QuizId.Value));
        if (quiz == null)
        {
            throw new ArgumentException("Quiz not found.");
        }

        return new ScheduleDetailsDto
        {
            Id = schedule.Id.Value,
            Quiz = quiz,
            StartDate = schedule.StartDate,
            EndDate = schedule.EndDate,
        };
    }
}