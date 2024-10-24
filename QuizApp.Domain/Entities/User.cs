using System.ComponentModel.DataAnnotations;

namespace QuizApp.Domain.Entities;

public class User
{
    [Key] [MaxLength(20)] public Guid UserId { get; set; } = Guid.Empty;
    [MaxLength(50)] public string FullName { get; set; } = string.Empty;
    [MaxLength(10)] public string Initial { get; set; } = string.Empty;
    [MaxLength(255)] public string Password { get; set; } = string.Empty;
    [MaxLength(10)] public string Role { get; set; } = string.Empty;
    public ICollection<UserSchedule> UserSchedules { get; } = new List<UserSchedule>();
    public ICollection<Response> Responses { get; } = new List<Response>();
    public ICollection<UserScore> Scores { get; } = new List<UserScore>();
}