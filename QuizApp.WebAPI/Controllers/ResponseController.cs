using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.DTOs.Response;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Response;

namespace QuizApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly IQueryHandler<GetUserResponsesInQuizQuery, ResponseDto> _getUserResponsesInQuizHandler;

        public ResponseController(IQueryHandler<GetUserResponsesInQuizQuery, ResponseDto> getUserResponsesInQuizHandler)
        {
            _getUserResponsesInQuizHandler = getUserResponsesInQuizHandler;
        }

        [HttpGet("get-user-responses")]
        [Authorize]
        public async Task<IActionResult> GetUserResponses([FromBody] GetUserResponsesInQuizDto dto)
        {
            if (dto.QuizId == Guid.Empty || dto.UserId == Guid.Empty)
            {
                return BadRequest("All fields are required");
            }

            try
            {
                var responses = await _getUserResponsesInQuizHandler.HandleAsync(new GetUserResponsesInQuizQuery(dto));
                return Ok(responses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}