using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using QuizApp.Application.DTOs.User;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.User;
using QuizApp.Application.Services;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.User;

public class LoginUserHandler : IQueryHandler<LoginUserQuery, UserDetailsDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<Domain.Entities.User> _passwordHasher;
    private readonly JwtTokenService _jwtTokenService;

    public LoginUserHandler(IUserRepository userRepository, IPasswordHasher<Domain.Entities.User> passwordHasher,
        JwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<UserDetailsDto> HandleAsync(LoginUserQuery query)
    {
        if (!Regex.IsMatch(query.Initial, "^[A-Za-z]{2}[0-9][0-9]-[0-2]"))
            throw new ArgumentException("Invalid initial format.");

        var user = await _userRepository.GetByInitialAsync(query.Initial);
        if (user == null || _passwordHasher.VerifyHashedPassword(user, user.Password, query.Password) !=
            PasswordVerificationResult.Success) throw new ArgumentException("Invalid initial or password.");

        var token = _jwtTokenService.GenerateToken(user.UserId.ToString(), user.Role);

        return new UserDetailsDto
        {
            UserId = user.UserId,
            FullName = user.FullName,
            Initial = user.Initial,
            Role = user.Role,
            Token = token
        };
    }
}