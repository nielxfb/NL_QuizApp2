using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Interfaces;

public interface IUserScoreRepository
{
    Task AddAsync(UserScore userScore);
    Task<UserScore?> GetAsync(Guid userId, Guid quizId);
}