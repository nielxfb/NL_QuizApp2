using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuizApp.Application.Commands;
using QuizApp.Application.Commands.Response;
using QuizApp.Application.DTOs.Response;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Response;

namespace QuizApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResponseController : ControllerBase
{
    private readonly IQueryHandler<GetUserResponsesInQuizQuery, ResponseDto> _getUserResponsesInQuizHandler;
    private readonly ICommandHandler<AddResponseCommand> _addResponseHandler;
    private readonly IMemoryCache _memoryCache;

    public ResponseController(IQueryHandler<GetUserResponsesInQuizQuery, ResponseDto> getUserResponsesInQuizHandler, ICommandHandler<AddResponseCommand> addResponseHandler, IMemoryCache memoryCache)
    {
        _getUserResponsesInQuizHandler = getUserResponsesInQuizHandler;
        _addResponseHandler = addResponseHandler;
        _memoryCache = memoryCache;
    }

    [HttpGet("get-user-responses")]
    [Authorize]
    public async Task<IActionResult> GetUserResponses([FromQuery] GetUserResponsesInQuizDto dto)
    {
        if (dto.ScheduleId == Guid.Empty || dto.UserId == Guid.Empty) return BadRequest("All fields are required");

        try
        {
            if (_memoryCache.TryGetValue(new { dto.ScheduleId, dto.UserId }, out var responses))
                return Ok(responses);
            
            responses = await _getUserResponsesInQuizHandler.HandleAsync(new GetUserResponsesInQuizQuery(dto));
            _memoryCache.Set(new { dto.ScheduleId, dto.UserId }, responses, TimeSpan.FromMinutes(5));
            return Ok(responses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("add-response")]
    [Authorize]
    public async Task<IActionResult> AddResponse([FromBody] AddResponseDto dto)
    {
        if (dto.ScheduleId == Guid.Empty || dto.UserId == Guid.Empty || dto.QuestionId == Guid.Empty ||
            dto.OptionChoice == "") return BadRequest("All fields are required");

        try
        {
            await _addResponseHandler.HandleAsync(new AddResponseCommand(dto));
            return Ok("Successfully added response!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}