using System.Net.Http.Headers;
using QuizApp.Application.DTOs.Quiz;
using QuizApp.Blazor2.Modules;
using QuizApp.Blazor2.Utils;

namespace QuizApp.Blazor2.Services;

public class QuizService
{
    private readonly HttpClient _httpClient;
    private readonly ICookie _cookie;

    public QuizService(HttpClient httpClient, ICookie cookie)
    {
        _httpClient = httpClient;
        _cookie = cookie;
    }

    public async Task<Response<List<QuizDetailsDto>>> GetQuizzes()
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<List<QuizDetailsDto>>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);
        var response = await _httpClient.GetAsync("api/Quiz/get-all-quizzes");

        if (!response.IsSuccessStatusCode)
        {
            var message = await response.Content.ReadAsStringAsync();
            return new Response<List<QuizDetailsDto>>
            {
                IsSuccess = false,
                Message = message,
            };
        }
        
        var content = await response.Content.ReadFromJsonAsync<List<QuizDetailsDto>>();
        return new Response<List<QuizDetailsDto>>
        {
            IsSuccess = true,
            Data = content,
        };
    }

    public async Task<Response<QuizDetailsDto>> GetQuizById(string id)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<QuizDetailsDto>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }
        
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        if (!Guid.TryParse(id, out _))
        {
            return new Response<QuizDetailsDto>
            {
                IsSuccess = false,
                Message = "Invalid id",
            };
        }
        
        var response = await _httpClient.GetAsync($"api/Quiz/get-quiz-by-id?id={id}");
        if (!response.IsSuccessStatusCode)
        {
            var message = await response.Content.ReadAsStringAsync();
            return new Response<QuizDetailsDto>
            {
                IsSuccess = false,
                Message = message,
            };
        }
        
        var content = await response.Content.ReadFromJsonAsync<QuizDetailsDto>();
        return new Response<QuizDetailsDto>
        {
            IsSuccess = true,
            Data = content,
        };
    }
}