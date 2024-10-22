namespace QuizApp.Application.DTOs.User;

public class RegisterUserDto
{
    public string FullName { get; set; } = string.Empty;
    public string Initial { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}