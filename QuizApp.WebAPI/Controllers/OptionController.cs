using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Commands.Option;
using QuizApp.Application.DTOs.Option;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Option;

namespace QuizApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly ICommandHandler<AddOptionCommand> _addOptionHandler;
        private readonly ICommandHandler<RemoveOptionCommand> _removeOptionHandler;
        private readonly ICommandHandler<UpdateOptionCommand> _updateOptionHandler;
        private readonly IQueryHandler<GetByQuestionIdQuery, List<OptionDto>> _getByQuestionIdHandler;

        public OptionController(ICommandHandler<AddOptionCommand> addOptionHandler, ICommandHandler<RemoveOptionCommand> removeOptionHandler, ICommandHandler<UpdateOptionCommand> updateOptionHandler, IQueryHandler<GetByQuestionIdQuery, List<OptionDto>> getByQuestionIdHandler)
        {
            _addOptionHandler = addOptionHandler;
            _removeOptionHandler = removeOptionHandler;
            _updateOptionHandler = updateOptionHandler;
            _getByQuestionIdHandler = getByQuestionIdHandler;
        }
        
        [HttpPost("add-option")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddOption([FromBody] AddOptionDto dto)
        {
            if (dto.QuestionId == Guid.Empty || dto.OptionChoice == "" || dto.OptionText == "")
            {
                return BadRequest("All fields are required");
            }

            if (dto.OptionChoice.Length > 1)
            {
                return BadRequest("Option choice must be a single character!");
            }

            try
            {
                await _addOptionHandler.HandleAsync(new AddOptionCommand(dto));
                return Ok("Successfully added option!");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        [HttpDelete("remove-option")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveOption([FromBody] RemoveOptionDto dto)
        {
            if (dto.QuestionId == Guid.Empty || dto.OptionChoice == "")
            {
                return BadRequest("All fields are required");
            }
            
            if (dto.OptionChoice.Length > 1)
            {
                return BadRequest("Option choice must be a single character!");
            }

            try
            {
                await _removeOptionHandler.HandleAsync(new RemoveOptionCommand(dto));
                return Ok("Successfully removed option!");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        
        [HttpPut("update-option")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOption([FromBody] UpdateOptionDto dto)
        {
            if (dto.QuestionId == Guid.Empty || dto.OptionChoice == "" || dto.OptionText == "")
            {
                return BadRequest("All fields are required");
            }
            
            if (dto.OptionChoice.Length > 1)
            {
                return BadRequest("Option choice must be a single character!");
            }

            try
            {
                await _updateOptionHandler.HandleAsync(new UpdateOptionCommand(dto));
                return Ok("Successfully updated option!");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("get-by-question-id")]
        [Authorize]
        public async Task<IActionResult> GetByQuestionId([FromQuery] Guid questionId)
        {
            if (questionId == Guid.Empty)
            {
                return BadRequest("Question ID is required");
            }

            try
            {
                var options = await _getByQuestionIdHandler.HandleAsync(new GetByQuestionIdQuery(questionId));
                return Ok(options);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
