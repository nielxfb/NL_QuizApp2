namespace QuizApp.Domain.Entities;

public class User
{
    public UserId Id { get; set; } = null!;
    public string FullName { get; set; } = string.Empty;
    public string Initial { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public ICollection<Response> Responses = new List<Response>();
    public ICollection<UserSchedule> UserSchedules = new List<UserSchedule>();
}