using System.Net.Http.Headers;
using QuizApp.Application.DTOs.UserScore;
using QuizApp.Blazor2.Modules;
using QuizApp.Blazor2.Utils;
using QuizApp.Domain.Entities;

namespace QuizApp.Blazor2.Services;

public class UserScoreService
{
    private readonly HttpClient _httpClient;
    private readonly ICookie _cookie;

    public UserScoreService(HttpClient httpClient, ICookie cookie)
    {
        _httpClient = httpClient;
        _cookie = cookie;
    }
    
    public async Task<Response<string>> AddUserScoreAsync(AddUserScoreDto addUserScoreDto)
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
            var response = await _httpClient.PostAsJsonAsync("api/UserScore/add-score", addUserScoreDto);
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
                Message = $"An error has occured: {e.Message}",
            };
        }
    }

    public async Task<Response<UserScoreDto>> GetUserScore(GetUserScoreDto dto)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<UserScoreDto>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var response = await _httpClient.GetAsync($"api/UserScore/get-user-score?userId={dto.UserId}&scheduleId={dto.ScheduleId}");
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new Response<UserScoreDto>
                {
                    IsSuccess = false,
                    Message = message,
                };
            }

            var content = await response.Content.ReadFromJsonAsync<UserScoreDto>();
            return new Response<UserScoreDto>
            {
                IsSuccess = true,
                Data = content,
            };
        }
        catch (Exception e)
        {
            return new Response<UserScoreDto>
            {
                IsSuccess = false,
                Message = $"An error has occured: {e.Message}",
            };
        }
    }

    public async Task<Response<List<UserScoreDto>>> GetScoreByUser(Guid UserId)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<List<UserScoreDto>>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var response = await _httpClient.GetAsync($"api/UserScore/get-by-user?userId={UserId}");
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new Response<List<UserScoreDto>>
                {
                    IsSuccess = false,
                    Message = message,
                };
            }
            
            var content = await response.Content.ReadFromJsonAsync<List<UserScoreDto>>();
            return new Response<List<UserScoreDto>>
            {
                IsSuccess = true,
                Data = content,
            };
        }
        catch (Exception e)
        {
            return new Response<List<UserScoreDto>>
            {
                IsSuccess = false,
                Message = $"An error has occured: {e.Message}",
            };
        }
    }
}