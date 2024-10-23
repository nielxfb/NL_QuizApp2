namespace QuizApp.Application.DTOs.User;

public class UserDetailsDto
{
    public Guid UserId { get; set; }
    public string Initial { get; set; }
    public string FullName { get; set; }
    public string Role { get; set; }
    public string Token { get; set; } = string.Empty;
}