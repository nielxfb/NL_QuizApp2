using System.Net.Http.Headers;
using QuizApp.Application.DTOs.UserSchedule;
using QuizApp.Blazor2.Modules;
using QuizApp.Blazor2.Utils;

namespace QuizApp.Blazor2.Services;

public class UserScheduleService
{
    private readonly HttpClient _httpClient;
    private readonly ICookie _cookie;

    public UserScheduleService(HttpClient httpClient, ICookie cookie)
    {
        _httpClient = httpClient;
        _cookie = cookie;
    }

    public async Task<Response<UsersInScheduleDto>> GetUsersInSchedule(string scheduleId)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<UsersInScheduleDto>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        if (!Guid.TryParse(scheduleId, out _))
        {
            return new Response<UsersInScheduleDto>
            {
                IsSuccess = false,
                Message = "Invalid schedule id.",
            };
        }

        try
        {
            var response =
                await _httpClient.GetAsync("api/UserSchedule/get-users-in-schedule?scheduleId=" + scheduleId);
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new Response<UsersInScheduleDto>
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    Message = message,
                };
            }

            var usersInSchedule = await response.Content.ReadFromJsonAsync<UsersInScheduleDto>();
            return new Response<UsersInScheduleDto>
            {
                IsSuccess = true,
                Data = usersInSchedule,
            };
        }
        catch (Exception e)
        {
            return new Response<UsersInScheduleDto>
            {
                IsSuccess = false,
                Message = e.Message,
            };
        }
    }

    public async Task<Response<List<UserSchedulesDto>>> GetUserSchedules(Guid userId)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<List<UserSchedulesDto>>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var response = await _httpClient.GetAsync("api/UserSchedule/get-user-schedules?userId=" + userId);
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new Response<List<UserSchedulesDto>>
                {
                    IsSuccess = false,
                    Message = message,
                };
            }

            var content = await response.Content.ReadFromJsonAsync<List<UserSchedulesDto>>();
            return new Response<List<UserSchedulesDto>>
            {
                IsSuccess = true,
                Data = content,
            };
        }
        catch (Exception e)
        {
            return new Response<List<UserSchedulesDto>>
            {
                IsSuccess = false,
                Message = "An error occurred: " + e.Message,
            };
        }
    }

    public async Task<Response<AddUserToScheduleDto>> AddUserToSchedule(AddUserToScheduleDto dto)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<AddUserToScheduleDto>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/UserSchedule/add-user-to-schedule", dto);

            if (response.IsSuccessStatusCode)
            {
                return new Response<AddUserToScheduleDto>
                {
                    IsSuccess = true,
                };
            }

            var message = await response.Content.ReadAsStringAsync();
            return new Response<AddUserToScheduleDto>
            {
                IsSuccess = false,
                Message = message,
            };
        }
        catch (Exception e)
        {
            return new Response<AddUserToScheduleDto>
            {
                IsSuccess = false,
                Message = e.Message,
            };
        }
    }

    public async Task<Response<RemoveUserFromScheduleDto>> RemoveUserFromSchedule(RemoveUserFromScheduleDto dto)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<RemoveUserFromScheduleDto>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var response = await _httpClient.DeleteAsync("api/UserSchedule/remove-user-from-schedule?userId=" +
                                                         dto.UserId + "&scheduleId=" + dto.ScheduleId);
            var message = await response.Content.ReadAsStringAsync();
            return new Response<RemoveUserFromScheduleDto>
            {
                IsSuccess = response.IsSuccessStatusCode,
                Message = message,
            };
        }
        catch (Exception e)
        {
            return new Response<RemoveUserFromScheduleDto>
            {
                IsSuccess = false,
                Message = $"An error occured: {e.Message}",
            };
        }
    }

    public async Task<Response<string>> UpdateStatus(UpdateStatusDto dto)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<string>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var response = await _httpClient.PutAsJsonAsync("api/UserSchedule/update-status", dto);
            var message = await response.Content.ReadAsStringAsync();
            return new Response<string>
            {
                IsSuccess = response.IsSuccessStatusCode,
                Message = message,
            };
        }
        catch (Exception e)
        {
            return new Response<string>
            {
                IsSuccess = false,
                Message = $"An error occured {e.Message}",
            };
        }
    }
}