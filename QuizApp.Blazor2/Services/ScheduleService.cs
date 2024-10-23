using System.Net.Http.Headers;
using QuizApp.Application.DTOs.Schedule;
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
}