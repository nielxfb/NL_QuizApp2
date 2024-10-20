using QuizApp.Application.DTOs;
using QuizApp.Application.DTOs.User;

namespace QuizApp.Application.Commands.User;

public class RegisterUserCommand
{
    public string FullName { get; set; }
    public string Initial { get; set; }
    public string Password { get; set; }

    public RegisterUserCommand(RegisterUserDto dto)
    {
        FullName = dto.FullName;
        Initial = dto.Initial;
        Password = dto.Password;
    }
}