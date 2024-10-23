using QuizApp.Application.DTOs.User;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.User;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.User;

public class GetUsersHandler : IQueryHandler<GetUsersQuery, List<UserDetailsDto>>
{
    private readonly IUserRepository _repository;

    public GetUsersHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserDetailsDto>> HandleAsync(GetUsersQuery query)
    {
        var users = await _repository.GetAllUsers();
        return users.Select(u => new UserDetailsDto
        {
            UserId = u.UserId,
            FullName = u.FullName,
            Initial = u.Initial,
            Role = u.Role,
        }).ToList();
    }
}