using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using QuizApp.Application.DTOs.User;
using QuizApp.Blazor2.Modules;
using QuizApp.Blazor2.Utils;
using Exception = System.Exception;

namespace QuizApp.Blazor2.Services;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly ICookie _cookie;
    private readonly ISessionStorageService _session;

    public UserService(HttpClient httpClient, ICookie cookie, ISessionStorageService session)
    {
        _httpClient = httpClient;
        _cookie = cookie;
        _session = session;
    }

    public async Task<Response<UserDetailsDto>> Login(LoginUserDto dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/login", dto);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<UserDetailsDto>();
                await _cookie.SetValue("user_cookie", content!.Token);
                await _session.SetItemAsync("user", new
                {
                    content.UserId,
                    content.Initial,
                    content.FullName,
                    content.Role,
                });
                return new Response<UserDetailsDto>
                {
                    IsSuccess = true,
                };
            }

            var message = await response.Content.ReadAsStringAsync();
            return new Response<UserDetailsDto>
            {
                IsSuccess = false,
                Message = message,
            };
        }
        catch (Exception ex)
        {
            return new Response<UserDetailsDto>
            {
                IsSuccess = false,
                Message = $"An error occurred: {ex.Message}",
            };
        }
    }

    public async Task<Response<UserDetailsDto>> Register(RegisterUserDto dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/register", dto);

            if (response.IsSuccessStatusCode)
            {
                return new Response<UserDetailsDto>
                {
                    IsSuccess = true,
                };
            }

            var error = await response.Content.ReadAsStringAsync();
            return new Response<UserDetailsDto>
            {
                IsSuccess = false,
                Message = error,
            };
        }
        catch (Exception ex)
        {
            return new Response<UserDetailsDto>
            {
                IsSuccess = false,
                Message = $"An error occurred: {ex.Message}",
            };
        }
    }

    public async Task<Response<UserDetailsDto>> GetUserDetails()
    {
        var user = await _session.GetItemAsync<UserDetailsDto>("user");
        var cookie = await _cookie.GetValue("user_cookie");

        if (user == null || cookie == "")
        {
            await _cookie.SetValue("user_cookie", "");
            await _session.RemoveItemAsync("user");
            return new Response<UserDetailsDto>
            {
                IsSuccess = false,
                Message = "User not logged in.",
            };
        }
        
        var response = await _httpClient.PostAsJsonAsync("api/User/login-by-cookie", new { Token = cookie });
        if (!response.IsSuccessStatusCode)
        {
            await _cookie.SetValue("user_cookie", "");
            return new Response<UserDetailsDto>
            {
                IsSuccess = false,
                Message = "User not logged in",
            };
        }

        user = await response.Content.ReadFromJsonAsync<UserDetailsDto>();
        await _session.SetItemAsync("user", new
        {
            user!.UserId,
            user.Initial,
            user.FullName,
            user.Role,
        });

        return new Response<UserDetailsDto>
        {
            IsSuccess = true,
            Message = "User logged in.",
            Data = user,
        };
    }

    public async Task LogOut()
    {
        await _session.RemoveItemAsync("user");
        await _cookie.SetValue("user_cookie", "");
    }
}