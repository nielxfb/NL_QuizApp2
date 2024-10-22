using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Commands.Schedule;
using QuizApp.Application.DTOs.Schedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Schedule;
using Exception = System.Exception;

namespace QuizApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController : ControllerBase
{
    private readonly IQueryHandler<GetSchedulesQuery, List<ScheduleDetailsDto>> _getSchedulesHandler;
    private readonly IQueryHandler<GetScheduleByIdQuery, ScheduleDetailsDto> _getScheduleByIdHandler;

    private readonly ICommandHandler<AddScheduleCommand> _addScheduleHandler;
    private readonly ICommandHandler<UpdateScheduleCommand> _updateScheduleHandler;
    private readonly ICommandHandler<RemoveScheduleCommand> _removeScheduleHandler;

    public ScheduleController(IQueryHandler<GetSchedulesQuery, List<ScheduleDetailsDto>> getSchedulesHandler,
        IQueryHandler<GetScheduleByIdQuery, ScheduleDetailsDto> getScheduleByIdHandler,
        ICommandHandler<AddScheduleCommand> addScheduleHandler,
        ICommandHandler<UpdateScheduleCommand> updateScheduleHandler,
        ICommandHandler<RemoveScheduleCommand> removeScheduleHandler)
    {
        _getSchedulesHandler = getSchedulesHandler;
        _getScheduleByIdHandler = getScheduleByIdHandler;
        _addScheduleHandler = addScheduleHandler;
        _updateScheduleHandler = updateScheduleHandler;
        _removeScheduleHandler = removeScheduleHandler;
    }

    [HttpGet("get-all-schedules")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllSchedules()
    {
        try
        {
            var schedules = await _getSchedulesHandler.HandleAsync(new GetSchedulesQuery());
            return Ok(schedules);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-schedule-by-id")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetScheduleById([FromQuery] Guid id)
    {
        if (id == Guid.Empty) return BadRequest("Id cannot be empty");

        try
        {
            var schedule = await _getScheduleByIdHandler.HandleAsync(new GetScheduleByIdQuery(id));
            return Ok(schedule);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("add-schedule")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddSchedule([FromBody] AddScheduleDto dto)
    {
        if (dto.QuizId == Guid.Empty) return BadRequest("QuizId cannot be empty");

        if (dto.StartDate == default) return BadRequest("StartDate cannot be empty");

        if (dto.EndDate == default) return BadRequest("EndDate cannot be empty");

        try
        {
            await _addScheduleHandler.HandleAsync(new AddScheduleCommand(dto));
            return Ok("Schedule added successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("update-schedule")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateSchedule([FromBody] UpdateScheduleDto dto)
    {
        if (dto.Id == Guid.Empty) return BadRequest("Id cannot be empty");

        if (dto.QuizId == Guid.Empty) return BadRequest("QuizId cannot be empty");

        if (dto.StartDate == default) return BadRequest("StartDate cannot be empty");

        if (dto.EndDate == default) return BadRequest("EndDate cannot be empty");

        try
        {
            await _updateScheduleHandler.HandleAsync(new UpdateScheduleCommand(dto));
            return Ok("Schedule updated successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("remove-schedule")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveSchedule([FromQuery] Guid id)
    {
        if (id == Guid.Empty) return BadRequest("Id cannot be empty");

        try
        {
            await _removeScheduleHandler.HandleAsync(new RemoveScheduleCommand(id));
            return Ok("Schedule removed successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}