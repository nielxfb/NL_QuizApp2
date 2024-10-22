using System.Net.Http.Headers;
using QuizApp.Application.DTOs.Question;
using QuizApp.Blazor2.Modules;
using QuizApp.Blazor2.Utils;

namespace QuizApp.Blazor2.Services;

public class QuestionService
{
    private readonly HttpClient _httpClient;
    private readonly ICookie _cookie;

    public QuestionService(HttpClient httpClient, ICookie cookie)
    {
        _httpClient = httpClient;
        _cookie = cookie;
    }

    public async Task<Response<List<QuestionDto>>> GetQuestionsInQuiz(string quizId)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<List<QuestionDto>>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var response = await _httpClient.GetAsync($"api/Question/get-questions-in-quiz?quizId={quizId}");

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new Response<List<QuestionDto>>
                {
                    IsSuccess = false,
                    Message = message,
                };
            }

            var content = await response.Content.ReadFromJsonAsync<List<QuestionDto>>();
            return new Response<List<QuestionDto>>
            {
                IsSuccess = true,
                Data = content,
            };
        }
        catch (Exception ex)
        {
            return new Response<List<QuestionDto>>
            {
                IsSuccess = false,
                Message = $"An error occurred: {ex.Message}",
            };
        }
    }
    
    public async Task<Response<string>> AddQuestion(AddQuestionDto dto)
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
            var response = await _httpClient.PostAsJsonAsync("api/Question/add-question", dto);

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = message,
                };
            }

            return new Response<string>
            {
                IsSuccess = true,
                Data = "Successfully added question!",
            };
        }
        catch (Exception ex)
        {
            return new Response<string>
            {
                IsSuccess = false,
                Message = $"An error occurred: {ex.Message}",
            };
        }
    }

    public async Task<Response<string>> UpdateQuestion(UpdateQuestionDto dto)
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
            var response = await _httpClient.PutAsJsonAsync("api/Question/update-question", dto);
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
                Message = $"An error occurred: {e.Message}",
            };
        }
    }

    public async Task<Response<string>> DeleteQuestion(string questionId)
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
            var response = await _httpClient.DeleteAsync("/api/Question/remove-question?questionId=" + questionId);
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
                Message = $"An error occurred: {e.Message}",
            };
        }
    }
}