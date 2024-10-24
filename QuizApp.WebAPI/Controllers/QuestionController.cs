using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QuizApp.Application.Commands.Question;
using QuizApp.Application.DTOs.Question;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Handlers.Question;
using QuizApp.Application.Queries.Question;

namespace QuizApp.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionController : ControllerBase
{
    private readonly ICommandHandler<AddQuestionCommand> _addCommandHandler;
    private readonly ICommandHandler<RemoveQuestionCommand> _removeQuestionHandler;
    private readonly ICommandHandler<UpdateQuestionCommand> _updateQuestionHandler;
    private readonly ICommandHandler<AddImageUrlCommand> _addImageUrlHandler;
    private readonly IQueryHandler<GetQuestionsInQuizQuery, List<QuestionDto>> _getQuestionsInQuizHandler;
    private readonly IMemoryCache _memoryCache;

    public QuestionController(ICommandHandler<AddQuestionCommand> addCommandHandler,
        ICommandHandler<RemoveQuestionCommand> removeQuestionHandler,
        ICommandHandler<UpdateQuestionCommand> updateQuestionHandler,
        ICommandHandler<AddImageUrlCommand> addImageUrlHandler,
        IQueryHandler<GetQuestionsInQuizQuery, List<QuestionDto>> getQuestionsInQuizHandler, IMemoryCache memoryCache)
    {
        _addCommandHandler = addCommandHandler;
        _removeQuestionHandler = removeQuestionHandler;
        _updateQuestionHandler = updateQuestionHandler;
        _addImageUrlHandler = addImageUrlHandler;
        _getQuestionsInQuizHandler = getQuestionsInQuizHandler;
        _memoryCache = memoryCache;
    }

    [HttpPost("add-question")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddQuestion([FromBody] AddQuestionDto dto)
    {
        if (dto.QuizId == Guid.Empty || dto.QuestionText == "") return BadRequest("All fields are required");

        try
        {
            await _addCommandHandler.HandleAsync(new AddQuestionCommand(dto));
            return Ok("Successfully added question!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("get-questions-in-quiz")]
    [Authorize]
    public async Task<IActionResult> GetQuestionsInQuiz([FromQuery] Guid quizId)
    {
        if (quizId == Guid.Empty) return BadRequest("Quiz Id is required");

        try
        {
            if (_memoryCache.TryGetValue(quizId, out List<QuestionDto>? questions))
                return Ok(questions);

            questions = await _getQuestionsInQuizHandler.HandleAsync(new GetQuestionsInQuizQuery(quizId));
            _memoryCache.Set(quizId, questions, TimeSpan.FromMinutes(10));
            return Ok(questions);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("remove-question")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveQuestion([FromQuery] Guid questionId)
    {
        if (questionId == Guid.Empty) return BadRequest("Question Id is required");

        try
        {
            await _removeQuestionHandler.HandleAsync(new RemoveQuestionCommand(questionId));
            return Ok("Successfully removed question!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("update-question")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionDto dto)
    {
        if (dto.QuestionId == Guid.Empty || dto.QuestionText == "") return BadRequest("All fields are required");

        try
        {
            await _updateQuestionHandler.HandleAsync(new UpdateQuestionCommand(dto));
            return Ok("Successfully updated question!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("add-image-url")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddImageUrl([FromBody] AddImageUrlDto dto)
    {
        if (dto.QuestionId == Guid.Empty || dto.ImageUrl == "") return BadRequest("All fields are required");

        try
        {
            await _addImageUrlHandler.HandleAsync(new AddImageUrlCommand(dto));
            return Ok("Successfully added image URL!");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}