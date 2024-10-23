using System.Net.Http.Headers;
using QuizApp.Application.DTOs.Option;
using QuizApp.Blazor2.Modules;
using QuizApp.Blazor2.Utils;

namespace QuizApp.Blazor2.Services;

public class OptionService
{
    private readonly HttpClient _httpClient;
    private readonly ICookie _cookie;

    public OptionService(HttpClient httpClient, ICookie cookie)
    {
        _httpClient = httpClient;
        _cookie = cookie;
    }

    public async Task<Response<AddOptionDto>> AddOption(AddOptionDto dto)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<AddOptionDto>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Option/add-option", dto);
            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Masuk sini");
                return new Response<AddOptionDto>
                {
                    IsSuccess = false,
                    Message = message,
                };
            }

            return new Response<AddOptionDto>
            {
                IsSuccess = true,
            };
        }
        catch (Exception e)
        {
            return new Response<AddOptionDto>
            {
                IsSuccess = false,
                Message = $"An error occurred: {e.Message}",
            };
        }
    }

    public async Task<Response<RemoveOptionDto>> RemoveOption(RemoveOptionDto dto)
    {
        var cookie = await _cookie.GetValue("user_cookie");

        if (cookie == "")
        {
            return new Response<RemoveOptionDto>
            {
                IsSuccess = false,
                Message = "You are not authorized to perform this action",
            };
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookie);

        try
        {
            var response =
                await _httpClient.DeleteAsync(
                    $"api/Option/remove-option?questionId={dto.QuestionId}&optionChoice={dto.OptionChoice}");
            var message = await response.Content.ReadAsStringAsync();
            return new Response<RemoveOptionDto>
            {
                IsSuccess = response.IsSuccessStatusCode,
                Message = message,
            };
        }
        catch (Exception e)
        {
            return new Response<RemoveOptionDto>
            {
                IsSuccess = false,
                Message = $"An error occurred: {e.Message}",
            };
        }
    }
}