using QuizApp.Application.DTOs.Schedule;

namespace QuizApp.Application.Commands.Schedule;

public class UpdateScheduleCommand
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public UpdateScheduleCommand(UpdateScheduleDto dto)
    {
        Id = dto.Id;
        QuizId = dto.QuizId;
        StartDate = dto.StartDate;
        EndDate = dto.EndDate;
    }
}