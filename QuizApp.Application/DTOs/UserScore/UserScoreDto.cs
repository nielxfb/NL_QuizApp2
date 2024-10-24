using QuizApp.Application.DTOs.Schedule;
using QuizApp.Application.DTOs.User;

namespace QuizApp.Application.DTOs.UserScore;

public class UserScoreDto
{
    public UserDetailsDto User { get; set; } = new();
    public ScheduleDetailsDto Schedule { get; set; } = new();
    public float Score { get; set; }
}