using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Commands.UserSchedule;
using QuizApp.Application.DTOs.UserSchedule;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.UserSchedule;

namespace QuizApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserScheduleController : ControllerBase
    {
        private readonly ICommandHandler<AddUserToScheduleCommand> _addUserToScheduleHandler;
        private readonly IQueryHandler<GetUsersInScheduleQuery, UsersInScheduleDto> _getUsersInScheduleHandler;

        public UserScheduleController(ICommandHandler<AddUserToScheduleCommand> addUserToScheduleHandler, IQueryHandler<GetUsersInScheduleQuery, UsersInScheduleDto> getUsersInScheduleHandler)
        {
            _addUserToScheduleHandler = addUserToScheduleHandler;
            _getUsersInScheduleHandler = getUsersInScheduleHandler;
        }

        [HttpGet("get-users-in-schedule")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersInSchedule([FromQuery] GetUsersInScheduleDto dto)
        {
            try
            {
                var userSchedules = await _getUsersInScheduleHandler.HandleAsync(new GetUsersInScheduleQuery(dto));
                return Ok(userSchedules);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPost("add-user-to-schedule")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserToSchedule([FromBody] AddUserToScheduleDto dto)
        {
            if (dto.UserId == Guid.Empty || dto.ScheduleId == Guid.Empty)
            {
                return BadRequest("Invalid user or schedule id.");
            }
            
            try
            {
                await _addUserToScheduleHandler.HandleAsync(new AddUserToScheduleCommand(dto));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
