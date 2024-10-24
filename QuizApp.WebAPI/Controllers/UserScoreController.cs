using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Commands.UserScore;
using QuizApp.Application.DTOs.UserScore;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.UserScore;

namespace QuizApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserScoreController : ControllerBase
    {
        private readonly ICommandHandler<AddUserScoreCommand> _addUserScoreHandler;
        private readonly IQueryHandler<GetScoresByUserQuery, List<UserScoreDto>> _getByUserHandler;
        private readonly IQueryHandler<GetUserScoreQuery, UserScoreDto> _getUserScoreHandler;

        public UserScoreController(ICommandHandler<AddUserScoreCommand> addUserScoreHandler,
            IQueryHandler<GetScoresByUserQuery, List<UserScoreDto>> getByUserHandler,
            IQueryHandler<GetUserScoreQuery, UserScoreDto> getUserScoreHandler)
        {
            _addUserScoreHandler = addUserScoreHandler;
            _getByUserHandler = getByUserHandler;
            _getUserScoreHandler = getUserScoreHandler;
        }

        [HttpGet("get-by-user")]
        [Authorize]
        public async Task<IActionResult> GetScoresByUser([FromQuery] GetScoresByUserDto dto)
        {
            if (dto.UserId == Guid.Empty)
            {
                return BadRequest("User ID is required.");
            }

            try
            {
                var response = await _getByUserHandler.HandleAsync(new GetScoresByUserQuery(dto));
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("get-user-score")]
        public async Task<IActionResult> GetUserScore([FromQuery] GetUserScoreDto dto)
        {
            if (dto.UserId == Guid.Empty || dto.ScheduleId == Guid.Empty)
            {
                return BadRequest("User ID and Schedule ID are required.");
            }

            try
            {
                var response = await _getUserScoreHandler.HandleAsync(new GetUserScoreQuery(dto));
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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