using System.ComponentModel.DataAnnotations;

namespace QuizApp.Domain.Entities;

public class Schedule
{
    [Key] [MaxLength(20)] public Guid ScheduleId { get; set; } = Guid.Empty;
    [MaxLength(20)] public Guid QuizId { get; set; } = Guid.Empty;
    public Quiz Quiz { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ICollection<UserSchedule> UserSchedules { get; } = new List<UserSchedule>();
    public ICollection<Response> Responses { get; } = new List<Response>();
    public ICollection<UserScore> UserScores { get; } = new List<UserScore>();
}