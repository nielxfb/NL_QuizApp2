using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    Task<User?> GetByInitialAsync(string initial);
    Task<List<User>> GetAllUsers();
    Task AddAsync(User user);
}