using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Commands;
using QuizApp.Application.Commands.Quiz;
using QuizApp.Application.DTOs;
using QuizApp.Application.DTOs.Quiz;
using QuizApp.Application.DTOs.User;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries;
using QuizApp.Application.Queries.Quiz;
using QuizApp.Domain.Entities;

namespace QuizApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQueryHandler<GetQuizzesQuery, List<QuizDetailsDto>> _getQuizzesHandler;
        private readonly IQueryHandler<GetQuizByIdQuery, QuizDetailsDto> _getQuizByIdHandler;
        
        private readonly ICommandHandler<AddQuizCommand> _addQuizHandler;
        private readonly ICommandHandler<RemoveQuizCommand> _removeQuizHandler;
        private readonly ICommandHandler<UpdateQuizCommand> _updateQuizHandler;

        public QuizController(IQueryHandler<GetQuizzesQuery, List<QuizDetailsDto>> getQuizzesHandler, IQueryHandler<GetQuizByIdQuery, QuizDetailsDto> getQuizByIdHandler, ICommandHandler<AddQuizCommand> addQuizHandler, ICommandHandler<RemoveQuizCommand> removeQuizHandler, ICommandHandler<UpdateQuizCommand> updateQuizHandler)
        {
            _getQuizzesHandler = getQuizzesHandler;
            _getQuizByIdHandler = getQuizByIdHandler;
            _addQuizHandler = addQuizHandler;
            _removeQuizHandler = removeQuizHandler;
            _updateQuizHandler = updateQuizHandler;
        }

        [HttpGet("get-all-quizzes")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetQuizzesAsync()
        {
            try
            {
                var quizzes = await _getQuizzesHandler.HandleAsync(new GetQuizzesQuery());
                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("get-quiz-by-id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetQuizByIdAsync([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id cannot be empty");
            }

            try
            {
                var quiz = await _getQuizByIdHandler.HandleAsync(new GetQuizByIdQuery(id));
                return Ok(quiz);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-new-quiz")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddQuizAsync([FromBody] AddQuizDto dto)
        {
            if (dto.Title == "")
            {
                return BadRequest("Title cannot be empty");
            }

            try
            {
                await _addQuizHandler.HandleAsync(new AddQuizCommand(dto));
                return Ok("Successfully added Quiz");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-quiz")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteQuizAsync([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Id cannot be empty");
            }

            try
            {
                await _removeQuizHandler.HandleAsync(new RemoveQuizCommand(id));
                return Ok("Successfully removed Quiz");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-quiz")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateQuizAsync([FromBody] UpdateQuizDto dto)
        {
            if (dto.Id == Guid.Empty || dto.Title == "")
            {
                return BadRequest("Id and Title cannot be empty");
            }

            try
            {
                await _updateQuizHandler.HandleAsync(new UpdateQuizCommand(dto));
                return Ok("Successfully updated Quiz");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}