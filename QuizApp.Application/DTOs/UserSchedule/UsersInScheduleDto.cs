using QuizApp.Application.DTOs.User;

namespace QuizApp.Application.DTOs.UserSchedule;

public class UsersInScheduleDto
{
    public Guid ScheduleId { get; set; }
    public ICollection<UserDto> Users { get; set; }
}