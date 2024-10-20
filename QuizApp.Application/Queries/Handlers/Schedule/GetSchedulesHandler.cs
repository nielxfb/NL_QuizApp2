using System.Diagnostics;
using QuizApp.Application.DTOs.Quiz;
using QuizApp.Application.DTOs.Schedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Schedule;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.Schedule;

public class GetSchedulesHandler : IQueryHandler<GetSchedulesQuery, List<ScheduleDetailsDto>>
{
    private readonly IScheduleRepository _scheduleRepository;

    public GetSchedulesHandler(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task<List<ScheduleDetailsDto>> HandleAsync(GetSchedulesQuery query)
    {
        var schedules = await _scheduleRepository.GetAllAsync();

        return schedules.Select(schedule => new ScheduleDetailsDto
        {
            Id = schedule.Id.Value,
            Quiz = schedule.Quiz,
            StartDate = schedule.StartDate,
            EndDate = schedule.EndDate,
        }).ToList();
    }
}