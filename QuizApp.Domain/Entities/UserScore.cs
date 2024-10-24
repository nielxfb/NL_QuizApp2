using Microsoft.EntityFrameworkCore;

namespace QuizApp.Domain.Entities;

[PrimaryKey(nameof(UserId), nameof(QuizId))]
public class UserScore
{
    public Guid UserId { get; set; } = Guid.Empty;
    public User User { get; set; } = null!;
    public Guid QuizId { get; set; } = Guid.Empty;
    public Quiz Quiz { get; set; } = null!;
    public float Score { get; set; }
}