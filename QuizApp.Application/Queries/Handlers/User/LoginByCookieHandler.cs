using QuizApp.Application.DTOs.User;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.User;
using QuizApp.Application.Services;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.User;

public class LoginByCookieHandler : IQueryHandler<LoginByCookieQuery, UserDetailsDto>
{
    private readonly IUserRepository _repository;
    private readonly JwtTokenService _jwtTokenService;

    public LoginByCookieHandler(IUserRepository repository, JwtTokenService jwtTokenService)
    {
        _repository = repository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<UserDetailsDto> HandleAsync(LoginByCookieQuery query)
    {
        var userId = _jwtTokenService.ValidateToken(query.Token);
        var user = await _repository.GetByIdAsync(new Guid(userId));
        if (user == null) throw new ArgumentException("Invalid token.");
        
        return new UserDetailsDto
        {
            UserId = user.UserId,
            FullName = user.FullName,
            Initial = user.Initial,
            Role = user.Role,
            Token = query.Token
        };
    }
}