using Microsoft.EntityFrameworkCore;

namespace QuizApp.Domain.Entities;

[PrimaryKey(nameof(UserId), nameof(ScheduleId))]
public class UserScore
{
    public Guid UserId { get; set; } = Guid.Empty;
    public User User { get; set; } = null!;
    public Guid ScheduleId { get; set; } = Guid.Empty;
    public Schedule Schedule { get; set; } = null!;
    public float Score { get; set; }
}