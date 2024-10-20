using QuizApp.Application.DTOs.User;

namespace QuizApp.Application.Queries.User;

public class LoginUserQuery
{
    public string Initial { get; set; }
    public string Password { get; set; }

    public LoginUserQuery(LoginUserDto dto)
    {
        Initial = dto.Initial;
        Password = dto.Password;
    }
}