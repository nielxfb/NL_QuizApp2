using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId userId);
    Task<User?> GetByInitialAsync(string initial);
    Task AddAsync(User user);
}