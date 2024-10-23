using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Commands.UserSchedule;
using QuizApp.Application.DTOs.UserSchedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.UserSchedule;

namespace QuizApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserScheduleController : ControllerBase
{
    private readonly ICommandHandler<AddUserToScheduleCommand> _addUserToScheduleHandler;
    private readonly ICommandHandler<RemoveUserFromScheduleCommand> _removeUserFromScheduleHandler;
    private readonly IQueryHandler<GetUsersInScheduleQuery, UsersInScheduleDto> _getUsersInScheduleHandler;
    private readonly IQueryHandler<GetUserSchedulesQuery, UserSchedulesDto> _getUserSchedulesHandler;

    public UserScheduleController(ICommandHandler<AddUserToScheduleCommand> addUserToScheduleHandler,
        ICommandHandler<RemoveUserFromScheduleCommand> removeUserFromScheduleHandler,
        IQueryHandler<GetUsersInScheduleQuery, UsersInScheduleDto> getUsersInScheduleHandler,
        IQueryHandler<GetUserSchedulesQuery, UserSchedulesDto> getUserSchedulesHandler)
    {
        _addUserToScheduleHandler = addUserToScheduleHandler;
        _removeUserFromScheduleHandler = removeUserFromScheduleHandler;
        _getUsersInScheduleHandler = getUsersInScheduleHandler;
        _getUserSchedulesHandler = getUserSchedulesHandler;
    }

    [HttpGet("get-users-in-schedule")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsersInSchedule([FromQuery] GetUsersInScheduleDto dto)
    {
        try
        {
            var userSchedules = await _getUsersInScheduleHandler.HandleAsync(new GetUsersInScheduleQuery(dto));
            return Ok(userSchedules);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-user-schedules")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserSchedules([FromQuery] GetUserSchedulesDto dto)
    {
        try
        {
            var userSchedules = await _getUserSchedulesHandler.HandleAsync(new GetUserSchedulesQuery(dto));
            return Ok(userSchedules);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("add-user-to-schedule")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddUserToSchedule([FromBody] AddUserToScheduleDto dto)
    {
        if (dto.UserId == Guid.Empty || dto.ScheduleId == Guid.Empty) return BadRequest("Invalid user or schedule id.");

        try
        {
            await _addUserToScheduleHandler.HandleAsync(new AddUserToScheduleCommand(dto));
            return Ok("Successfully added user to schedule.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("remove-user-from-schedule")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveUserFromSchedule([FromQuery] RemoveUserFromScheduleDto dto)
    {
        if (dto.UserId == Guid.Empty || dto.ScheduleId == Guid.Empty) return BadRequest("Invalid user or schedule id.");

        try
        {
            await _removeUserFromScheduleHandler.HandleAsync(new RemoveUserFromScheduleCommand(dto));
            return Ok("Successfully removed user from schedule.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}