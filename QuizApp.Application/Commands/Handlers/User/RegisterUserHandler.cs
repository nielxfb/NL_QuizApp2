using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using QuizApp.Application.Commands.User;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.User;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<Domain.Entities.User> _passwordHasher;

    public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher<Domain.Entities.User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task HandleAsync(RegisterUserCommand command)
    {
        if (!Regex.IsMatch(command.Initial, "^[A-Za-z]{2}[0-9][0-9]-[0-2]"))
            throw new ArgumentException("Invalid initial format.");

        if (await _userRepository.GetByInitialAsync(command.Initial) != null)
            throw new ArgumentException("User with this initial already exists.");

        var user = new Domain.Entities.User
        {
            UserId = Guid.NewGuid(),
            FullName = command.FullName,
            Initial = command.Initial,
            Role = "User"
        };

        user.Password = _passwordHasher.HashPassword(user, command.Password);
        await _userRepository.AddAsync(user);
    }
}