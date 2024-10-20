using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace QuizApp.Domain.Entities;

[PrimaryKey(nameof(UserId), nameof(ScheduleId))]
public class UserSchedule
{
    [MaxLength(20)] public Guid UserId { get; set; } = Guid.Empty;
    public User User { get; set; } = null!;
    [MaxLength(20)] public Guid ScheduleId { get; set; } = Guid.Empty;
    public Schedule Schedule { get; set; } = null!;
    [MaxLength(20)] public string Status { get; set; } = string.Empty;
}