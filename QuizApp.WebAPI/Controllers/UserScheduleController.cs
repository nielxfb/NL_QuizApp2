using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
    private readonly ICommandHandler<UpdateStatusCommand> _updateStatusHandler;
    private readonly IQueryHandler<GetUsersInScheduleQuery, UsersInScheduleDto> _getUsersInScheduleHandler;
    private readonly IQueryHandler<GetUserSchedulesQuery, List<UserSchedulesDto>> _getUserSchedulesHandler;
    private readonly IMemoryCache _memoryCache;

    public UserScheduleController(ICommandHandler<AddUserToScheduleCommand> addUserToScheduleHandler, ICommandHandler<RemoveUserFromScheduleCommand> removeUserFromScheduleHandler, ICommandHandler<UpdateStatusCommand> updateStatusHandler, IQueryHandler<GetUsersInScheduleQuery, UsersInScheduleDto> getUsersInScheduleHandler, IQueryHandler<GetUserSchedulesQuery, List<UserSchedulesDto>> getUserSchedulesHandler, IMemoryCache memoryCache)
    {
        _addUserToScheduleHandler = addUserToScheduleHandler;
        _removeUserFromScheduleHandler = removeUserFromScheduleHandler;
        _updateStatusHandler = updateStatusHandler;
        _getUsersInScheduleHandler = getUsersInScheduleHandler;
        _getUserSchedulesHandler = getUserSchedulesHandler;
        _memoryCache = memoryCache;
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
    [Authorize]
    public async Task<IActionResult> GetUserSchedules([FromQuery] GetUserSchedulesDto dto)
    {
        if (dto.UserId == Guid.Empty) return BadRequest("Invalid user id.");
        
        try
        {
            if (_memoryCache.TryGetValue(dto.UserId, out List<UserSchedulesDto>? userSchedules))
                return Ok(userSchedules);
            
            userSchedules = await _getUserSchedulesHandler.HandleAsync(new GetUserSchedulesQuery(dto));
            _memoryCache.Set(dto.UserId, userSchedules, TimeSpan.FromMinutes(5));
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
    
    [HttpPut("update-status")]
    [Authorize]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDto dto)
    {
        if (dto.UserId == Guid.Empty || dto.ScheduleId == Guid.Empty) return BadRequest("Invalid user or schedule id.");

        try
        {
            await _updateStatusHandler.HandleAsync(new UpdateStatusCommand(dto));
            return Ok("Successfully updated user schedule status.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}