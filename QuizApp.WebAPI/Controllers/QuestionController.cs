using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Commands.Question;
using QuizApp.Application.DTOs.Question;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Handlers.Question;
using QuizApp.Application.Queries.Question;

namespace QuizApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ICommandHandler<AddQuestionCommand> _addCommandHandler;
        private readonly ICommandHandler<RemoveQuestionCommand> _removeQuestionHandler;
        private readonly ICommandHandler<UpdateQuestionCommand> _updateQuestionHandler;
        private readonly IQueryHandler<GetQuestionsInQuizQuery, List<QuestionDto>> _getQuestionsInQuizHandler;

        public QuestionController(ICommandHandler<AddQuestionCommand> addCommandHandler,
            ICommandHandler<RemoveQuestionCommand> removeQuestionHandler,
            ICommandHandler<UpdateQuestionCommand> updateQuestionHandler,
            IQueryHandler<GetQuestionsInQuizQuery, List<QuestionDto>> getQuestionsInQuizHandler)
        {
            _addCommandHandler = addCommandHandler;
            _removeQuestionHandler = removeQuestionHandler;
            _updateQuestionHandler = updateQuestionHandler;
            _getQuestionsInQuizHandler = getQuestionsInQuizHandler;
        }

        [HttpPost("add-question")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddQuestion([FromBody] AddQuestionDto dto)
        {
            if (dto.QuizId == Guid.Empty || dto.QuestionText == "")
            {
                return BadRequest("All fields are required");
            }
            
            try
            {
                await _addCommandHandler.HandleAsync(new AddQuestionCommand(dto));
                return Ok("Successfully added question!");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("get-questions-in-quiz")]
        [Authorize]
        public async Task<IActionResult> GetQuestionsInQuiz([FromQuery] Guid quizId)
        {
            if (quizId == Guid.Empty)
            {
                return BadRequest("Quiz Id is required");
            }

            try
            {
                var questions = await _getQuestionsInQuizHandler.HandleAsync(new GetQuestionsInQuizQuery(quizId));
                return Ok(questions);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        [HttpDelete("remove-question")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveQuestion([FromQuery] Guid questionId)
        {
            if (questionId == Guid.Empty)
            {
                return BadRequest("Question Id is required");
            }

            try
            {
                await _removeQuestionHandler.HandleAsync(new RemoveQuestionCommand(questionId));
                return Ok("Successfully removed question!");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut("update-question")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionDto dto)
        {
            if (dto.QuestionId == Guid.Empty || dto.QuestionText == "")
            {
                return BadRequest("All fields are required");
            }

            try
            {
                await _updateQuestionHandler.HandleAsync(new UpdateQuestionCommand(dto));
                return Ok("Successfully updated question!");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}