using QuizApp.Application.DTOs.User;

namespace QuizApp.Application.Queries.User;

public class LoginByCookieQuery
{
    public string Token { get; set; }

    public LoginByCookieQuery(LoginByCookieDto dto)
    {
        Token = dto.Token;
    }
}