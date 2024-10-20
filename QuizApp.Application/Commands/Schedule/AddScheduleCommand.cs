using QuizApp.Application.DTOs.Schedule;

namespace QuizApp.Application.Commands.Schedule;

public class AddScheduleCommand
{
    public Guid QuizId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public AddScheduleCommand(AddScheduleDto dto)
    {
        QuizId = dto.QuizId;
        StartDate = dto.StartDate;
        EndDate = dto.EndDate;
    }
}