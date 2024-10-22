using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    public ResponseController(IQueryHandler<GetUserResponsesInQuizQuery, ResponseDto> getUserResponsesInQuizHandler,
        ICommandHandler<AddResponseCommand> addResponseHandler)
    {
        _getUserResponsesInQuizHandler = getUserResponsesInQuizHandler;
        _addResponseHandler = addResponseHandler;
    }

    [HttpGet("get-user-responses")]
    [Authorize]
    public async Task<IActionResult> GetUserResponses([FromQuery] GetUserResponsesInQuizDto dto)
    {
        if (dto.QuizId == Guid.Empty || dto.UserId == Guid.Empty) return BadRequest("All fields are required");

        try
        {
            var responses = await _getUserResponsesInQuizHandler.HandleAsync(new GetUserResponsesInQuizQuery(dto));
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
        if (dto.QuizId == Guid.Empty || dto.UserId == Guid.Empty || dto.QuestionId == Guid.Empty ||
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