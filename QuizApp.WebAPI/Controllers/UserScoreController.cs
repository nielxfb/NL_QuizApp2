using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Commands.UserScore;
using QuizApp.Application.DTOs.UserScore;
using QuizApp.Application.Interfaces.Handlers;

namespace QuizApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserScoreController : ControllerBase
    {
        private readonly ICommandHandler<AddUserScoreCommand> _addUserScoreHandler;

        public UserScoreController(ICommandHandler<AddUserScoreCommand> addUserScoreHandler)
        {
            _addUserScoreHandler = addUserScoreHandler;
        }

        [HttpPost("add-score")]
        [Authorize]
        public async Task<IActionResult> AddUserScore([FromBody] AddUserScoreDto dto)
        {
            if (dto.UserId == Guid.Empty || dto.ScheduleId == Guid.Empty)
            {
                return BadRequest("User ID and Schedule ID are required.");
            }

            if (dto.QuestionCount == 0)
            {
                return BadRequest("Question count is required.");
            }
            
            if (dto.SelectedOptions.Count == 0)
            {
                return BadRequest("Selected options are required.");
            }
            
            if (dto.SelectedOptions.Count > dto.QuestionCount)
            {
                return BadRequest("Selected options count cannot be greater than question count.");
            }

            try
            {
                var command = new AddUserScoreCommand(dto);
                await _addUserScoreHandler.HandleAsync(command);
                return Ok("Successfully added user score");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
