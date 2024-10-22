using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Commands;
using QuizApp.Application.Commands.Handlers;
using QuizApp.Application.Commands.User;
using QuizApp.Application.DTOs;
using QuizApp.Application.DTOs.User;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries;
using QuizApp.Application.Queries.Handlers;
using QuizApp.Application.Queries.User;

namespace QuizApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ICommandHandler<RegisterUserCommand> _registerUserHandler;

    private readonly IQueryHandler<LoginUserQuery, UserDetailsDto> _loginUserHandler;

    public UserController(ICommandHandler<RegisterUserCommand> registerUserHandler,
        IQueryHandler<LoginUserQuery, UserDetailsDto> loginUserHandler)
    {
        _registerUserHandler = registerUserHandler;
        _loginUserHandler = loginUserHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto dto)
    {
        if (dto.FullName == "" || dto.Initial == "" || dto.Password == "")
            return BadRequest("Please provide all required fields!");

        try
        {
            await _registerUserHandler.HandleAsync(new RegisterUserCommand(dto));
            return Ok("Successfully registered user!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto dto)
    {
        if (dto.Initial == "" || dto.Password == "") return BadRequest("Please provide all required fields!");

        try
        {
            var userDetails = await _loginUserHandler.HandleAsync(new LoginUserQuery(dto));
            return Ok(userDetails);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}