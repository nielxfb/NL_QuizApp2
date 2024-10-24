using System.Net.Http.Headers;
using QuizApp.Application.DTOs.Response;
using QuizApp.Application.DTOs.Schedule;
using QuizApp.Blazor2.Modules;
using QuizApp.Blazor2.Utils;

namespace QuizApp.Blazor2.Services;

public class ResponseService
{
    private readonly HttpClient _httpClient;
    private readonly ICookie _cookie;

    public ResponseService(HttpClient httpClient, ICookie cookie)
    {
        _httpClient = httpClient;
        _cookie = cookie;
    }

    public async Task<Response<ResponseDto>> GetUserResponses(GetUserResponsesInQuizDto dto)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<ResponseDto>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var response =
                await _httpClient.GetAsync($"api/Response/get-user-responses?userId={dto.UserId}&scheduleId={dto.ScheduleId}");
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new Response<ResponseDto>
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    Message = message,
                };
            }
            
            var userResponses = await response.Content.ReadFromJsonAsync<ResponseDto>();
            return new Response<ResponseDto>
            {
                IsSuccess = true,
                Data = userResponses,
            };
        }
        catch (Exception e)
        {
            return new Response<ResponseDto>
            {
                IsSuccess = false,
                Message = $"An error occured: {e.Message}",
            };
        }
    }
}