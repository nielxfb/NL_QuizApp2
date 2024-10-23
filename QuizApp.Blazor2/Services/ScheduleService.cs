using System.Net.Http.Headers;
using QuizApp.Application.DTOs.Schedule;
using QuizApp.Application.DTOs.UserSchedule;
using QuizApp.Blazor2.Modules;
using QuizApp.Blazor2.Utils;

namespace QuizApp.Blazor2.Services;

public class ScheduleService
{
    private readonly HttpClient _httpClient;
    private readonly ICookie _cookie;

    public ScheduleService(HttpClient httpClient, ICookie cookie)
    {
        _httpClient = httpClient;
        _cookie = cookie;
    }

    public async Task<Response<List<ScheduleDetailsDto>>> GetSchedules()
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<List<ScheduleDetailsDto>>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var response = await _httpClient.GetAsync("api/Schedule/get-all-schedules");
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new Response<List<ScheduleDetailsDto>>
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    Message = message,
                };
            }

            var schedules = await response.Content.ReadFromJsonAsync<List<ScheduleDetailsDto>>();

            return new Response<List<ScheduleDetailsDto>>()
            {
                IsSuccess = true,
                Message = "Successfully retrieved schedules.",
                Data = schedules,
            };
        }
        catch (Exception e)
        {
            return new Response<List<ScheduleDetailsDto>>
            {
                IsSuccess = false,
                Message = e.Message,
            };
        }
    }

    public async Task<Response<AddScheduleDto>> AddSchedule(AddScheduleDto dto)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<AddScheduleDto>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var startDate = dto.StartDate.ToUniversalTime();
            var endDate = dto.EndDate.ToUniversalTime();
            dto.StartDate = startDate;
            dto.EndDate = endDate;
            var response = await _httpClient.PostAsJsonAsync("api/Schedule/add-schedule", dto);
            var message = await response.Content.ReadAsStringAsync();
            return new Response<AddScheduleDto>
            {
                IsSuccess = response.IsSuccessStatusCode,
                Message = message,
            };
        }
        catch (Exception e)
        {
            return new Response<AddScheduleDto>
            {
                IsSuccess = false,
                Message = e.Message,
            };
        }
    }

    public async Task<Response<UpdateScheduleDto>> UpdateSchedule(UpdateScheduleDto dto)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<UpdateScheduleDto>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var response = await _httpClient.PutAsJsonAsync("api/Schedule/update-schedule", dto);
            var message = await response.Content.ReadAsStringAsync();
            return new Response<UpdateScheduleDto>
            {
                IsSuccess = response.IsSuccessStatusCode,
                Message = message,
            };
        }
        catch (Exception e)
        {
            return new Response<UpdateScheduleDto>
            {
                IsSuccess = false,
                Message = e.Message,
            };
        }
    }

    public async Task<Response<string>> RemoveSchedule(Guid scheduleId)
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
            var response = await _httpClient.DeleteAsync($"api/Schedule/remove-schedule?id={scheduleId}");
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
                Message = e.Message,
            };
        }
    }

    public async Task<Response<ScheduleDetailsDto>> GetScheduleById(string scheduleId)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<ScheduleDetailsDto>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);
        
        if (!Guid.TryParse(scheduleId, out _))
        {
            return new Response<ScheduleDetailsDto>
            {
                IsSuccess = false,
                Message = "Invalid schedule id.",
            };
        }

        try
        {
            var response = await _httpClient.GetAsync("api/Schedule/get-schedule-by-id?id=" + scheduleId);
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new Response<ScheduleDetailsDto>
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    Message = message,
                };
            }

            var schedule = await response.Content.ReadFromJsonAsync<ScheduleDetailsDto>();
            return new Response<ScheduleDetailsDto>
            {
                IsSuccess = true,
                Message = "Successfully retrieved schedule.",
                Data = schedule,
            };
        }
        catch (Exception e)
        {
            return new Response<ScheduleDetailsDto>
            {
                IsSuccess = false,
                Message = e.Message,
            };
        }
    }

}